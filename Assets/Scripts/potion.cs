using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class potion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cellat geldi!!!");
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                //PhotonNetwork.Destroy(gameObject);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("Destroy", RpcTarget.All, gameObject);
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Cellat geldi2");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                //PhotonNetwork.Destroy(gameObject);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("Destroy", RpcTarget.All, gameObject);
            }
        }
    }

    [PunRPC]

    private void Destroy(GameObject gameObject)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
