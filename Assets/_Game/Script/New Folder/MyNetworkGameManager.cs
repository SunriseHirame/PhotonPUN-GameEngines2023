using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class MyNetworkGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject m_playerPrefab;


	public override void OnJoinedRoom()
    {
		SpawnPlayer();
	}

	private void SpawnPlayer()
    {
		if (PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
		{
			Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			var localPlayer = PhotonNetwork.Instantiate(m_playerPrefab.name, new Vector3(Random.Range(-4f, 4f), 0f, 0f), Quaternion.identity, 0);
			PlayerManager.LocalPlayerInstance = localPlayer;

		}
		else
		{

			Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
		}
	}

    private IEnumerator Start()
    {
		while (true)
        {
			if (PhotonNetwork.IsMasterClient)
			{
				if (PlayerManager.LocalPlayerInstance != null)
				{
					var color = Random.ColorHSV();
					PlayerManager.LocalPlayerInstance.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.All, color.r, color.g, color.b);
				}

			}
			yield return new WaitForSeconds(1f);
		}
	
    }
}
