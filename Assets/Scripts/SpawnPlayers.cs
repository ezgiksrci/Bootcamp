using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;
    PhotonView view;
    public static int pos;
    public static bool curveFollowAllowed = false;
    public static int numberPlayers = 0;

    private void Start()
    {
        numberPlayers = PhotonNetwork.CountOfPlayers;
        if (numberPlayers == 1)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position + transform.right * -2, Quaternion.identity);
            pos = -1;
        }
        else if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position + transform.right * 2, Quaternion.identity);
            pos = 1;
        }
    }
}
