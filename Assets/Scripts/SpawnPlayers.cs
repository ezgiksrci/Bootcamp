using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;
    PhotonView view;

    private void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position + transform.right * 2, Quaternion.identity);
    }
}
