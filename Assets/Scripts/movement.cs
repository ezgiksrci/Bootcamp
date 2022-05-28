using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class movement : MonoBehaviour
{

    [SerializeField] public static int pos = 0;             // -1: left, 0: middle(default); 1:right
    int sign;                                               // Direction of movement

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            // Determines which line the player swipe
            if (Input.GetKeyDown(KeyCode.A) && pos > -1)
            {
                pos--;
                sign = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D) && pos < 1)
            {
                pos++;
                sign = 1;
            }
        }
    }
}
