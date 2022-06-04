using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPotions : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> potions;
    void Start()
    {
        if(PhotonNetwork.PlayerList.Length == 1)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                var whichPotion = Random.Range(0, 3);
                PhotonNetwork.Instantiate(potions[whichPotion].name, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}
