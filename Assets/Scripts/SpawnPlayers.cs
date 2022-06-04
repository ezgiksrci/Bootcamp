using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public static int pos;          //Instantiated players are sent the pos info related to their spawn point
    
    private void Start()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position + transform.right * -2, Quaternion.identity);
            pos = -1;
        }
        else if (PhotonNetwork.PlayerList.Length == 2)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position + transform.right * 2, Quaternion.identity);
            pos = 1;
        }
    }
}
