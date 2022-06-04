using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class collectibles : MonoBehaviour
{
    public static bool fireSpell = false, iceSpell = false, shieldSpell = false;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!fireSpell && !iceSpell && !shieldSpell && view.IsMine) {
            
            if (other.gameObject.CompareTag("Fire Potion"))
            {
                if (other.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
                else
                {
                    PhotonView photonView = PhotonView.Get(this);
                    photonView.RPC("Destroy", RpcTarget.MasterClient, other.gameObject.GetComponent<PhotonView>().ViewID);
                }
                fireSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (other.gameObject.CompareTag("Ice Potion"))
            {
                if (other.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
                else
                {
                    PhotonView photonView = PhotonView.Get(this);
                    photonView.RPC("Destroy", RpcTarget.MasterClient, other.gameObject.GetComponent<PhotonView>().ViewID);
                }
                iceSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (other.gameObject.CompareTag("Shield Potion"))
            {
                if (other.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
                else
                {
                    PhotonView photonView = PhotonView.Get(this);
                    photonView.RPC("Destroy", RpcTarget.MasterClient, other.gameObject.GetComponent<PhotonView>().ViewID);
                }
                shieldSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    [PunRPC]

    private void Destroy(int ID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(ID).gameObject);
    }
}
