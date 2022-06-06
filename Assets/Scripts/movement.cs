using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class movement : MonoBehaviour
{

    [SerializeField] public static int pos;             // -1: left, 0: middle(default); 1:right
    int sign;             // Direction of movement
    bool speedCheck = false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

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
            // Swipe controls for mobile.

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if (endTouchPosition.x < startTouchPosition.x && pos > -1)
                {
                    pos--;
                    sign = -1;
                }
                else if (endTouchPosition.x > startTouchPosition.x && pos < 1)
                {
                    pos++;
                    sign = 1;
                }
            }


            //// Determines which line the player swipe
            //if (Input.GetKeyDown(KeyCode.A) && pos > -1)
            //{
            //    pos--;
            //    sign = -1;
            //}
            //else if (Input.GetKeyDown(KeyCode.D) && pos < 1)
            //{
            //    pos++;
            //    sign = 1;
            //}

            //if (Input.GetKey(KeyCode.W))
            //{
            //    curveFollow.speedModifier = 0.5f;
            //    speedCheck = true;
            //}
            //else 
            //{
            //    if (speedCheck)
            //    {
            //        curveFollow.speedModifier = 0.25f;
            //        speedCheck = false;
            //    }
            //    
            //}
        }
    }
}