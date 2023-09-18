using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MyNonSpeedRunLoader : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range (-4f, 4f), 0,0), Quaternion.identity);
        var color = Random.ColorHSV();
        player.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBufferedViaServer, color.r, color.g, color.b);  
    }
}
