using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shield : MonoBehaviour
{
    PhotonView view;
    GameObject Object;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("DisableShield", RpcTarget.All);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Fire Spell") || collision.gameObject.CompareTag("Ice Spell") || collision.gameObject.CompareTag("Shield Spell")))
        {
            Object = collision.gameObject;
            if (Object.GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(Object);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("DisableShield", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void DisableShield()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    void DestroySpell(GameObject gameObject)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
