using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPotions : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] List<GameObject> potions;
    private List<Transform> spawnPoints;

    void Start()
    {
        path = GameObject.Find("SpawnPotions");
        spawnPoints = new List<Transform>();

        for (int i = 0; i < path.transform.childCount; i++)
        {
            spawnPoints.Add(path.transform.GetChild(i));
        }

        if (PhotonNetwork.PlayerList.Length == 1)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                var whichPotion = Random.Range(0, 3);
                PhotonNetwork.Instantiate(potions[whichPotion].name, spawnPoints[i].position, Quaternion.identity);
                whichPotion = Random.Range(0, 3);
                PhotonNetwork.Instantiate(potions[whichPotion].name, spawnPoints[i].position + spawnPoints[i].transform.right * 4, Quaternion.identity);
                whichPotion = Random.Range(0, 3);
                PhotonNetwork.Instantiate(potions[whichPotion].name, spawnPoints[i].position + spawnPoints[i].transform.right * -4, Quaternion.identity);
            }
        }
    }
}
