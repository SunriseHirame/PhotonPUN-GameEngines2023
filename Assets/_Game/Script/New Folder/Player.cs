using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    [PunRPC]
    public void SetColor (float r, float g, float b)
    {
        GetComponentInChildren<MeshRenderer>().material.color = new Color(r, g, b);
    }
}
