using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class camera : MonoBehaviour
{
    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            gameObject.transform.Find("Camera").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").gameObject.SetActive(true);
        }
    }
}
