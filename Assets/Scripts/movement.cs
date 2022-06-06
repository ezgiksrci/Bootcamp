using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class movement : MonoBehaviour
{
    
    [SerializeField] public static int pos;             // -1: left, 0: middle(default); 1:right
    bool speedCheck = false;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        pos = SpawnPlayers.pos;
    }

    void Update()
    {
        if (view.IsMine)
        {
            // Determines which line the player swipe
            if (Input.GetKeyDown(KeyCode.A) && pos > -1)
            {
                pos--;
            }
            else if (Input.GetKeyDown(KeyCode.D) && pos < 1)
            {
                pos++;
            }

            if (Input.GetKey(KeyCode.W))
            {
                curveFollow.speedModifier = 0.5f;
                speedCheck = true;
            }
            else 
            {
                if (speedCheck)
                {
                    curveFollow.speedModifier = 0.25f;
                    speedCheck = false;
                }
                
            }
        }
    }
}
