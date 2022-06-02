using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class camera : MonoBehaviourPunCallbacks
{
    PhotonView view;
    int numberPlayers = 1;
    void Start()
    {
        view = GetComponent<PhotonView>();


        if (view.IsMine)
        {
            gameObject.transform.Find("Camera").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            new WaitForSeconds(3);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("curveFollowEnabled", RpcTarget.All);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        numberPlayers++;
    }

    [PunRPC]
    void curveFollowEnabled()
    {
        gameObject.GetComponent<curveFollow>().enabled = true;
        gameObject.GetComponent<camera>().enabled = false;
    }
}
