using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spellManagement : MonoBehaviour
{
    GameObject Object;
    PhotonView view;
    public static float speedModifier;
    private float iceSize, iceScale;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        iceScale = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Bir þey çarptý!!!");
        if (collision.gameObject.CompareTag("Ice Spell"))
        {
            print("Çarpan þey buzmuþ!!!");
            PhotonView photonView = PhotonView.Get(this);

            if (collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                print("1den geldi");
                PhotonNetwork.Destroy(collision.gameObject);
            }
            else
            {
                print("2den geldi");
                //photonView.RPC("Destroy", RpcTarget.MasterClient, collision.gameObject.GetComponent<PhotonView>().ViewID);
            }
            photonView.RPC("SpeedMod", RpcTarget.Others, 0f);
            photonView.RPC("IceTrapActivation", RpcTarget.All);
            photonView.RPC("Coroutine", RpcTarget.All);
        }

        else if (collision.gameObject.CompareTag("Fire Spell"))
        {
            print("Çarpan þey ateþmiþ!!!");
            PhotonView photonView = PhotonView.Get(this);

            if (collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                print("1den geldi");
                PhotonNetwork.Destroy(collision.gameObject);
            }
            else
            {
                print("2den geldi");
                //photonView.RPC("Destroy", RpcTarget.Others, collision.gameObject.GetComponent<PhotonView>().ViewID);
            }
            photonView.RPC("TParam", RpcTarget.Others, 0f);
            
        }

        else if (collision.gameObject.CompareTag("Building") && view.IsMine)
        {
            print("Binaya girdim!!!");
            PhotonView photonView = PhotonView.Get(this);
            curveFollow.routeToGo -= 1;
        }
    }

    private IEnumerator IceEffect()
    {
        yield return new WaitForSeconds(1);
        
        iceSize = gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale.y;
        while (iceSize > 0f)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("CrackIce", RpcTarget.All, iceSize);
                iceSize -= 0.2f;
            }
            yield return null;
        }
        
        iceSize = 1f;
        IceTrapDeActivation1();
    }

    private void IceTrapDeActivation1()
    {
        Debug.Log("IceTrapDeActivation1");
        if (view.IsMine)
        {
            Debug.Log("Buz Kapatma");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("IceTrapDeActivation", RpcTarget.All);
            photonView.RPC("SpeedMod", RpcTarget.All, 0.25f);
        }
    }

    [PunRPC]
    void SpeedMod(float a)
    {
        curveFollow.speedModifier = a;
    }

    [PunRPC]
    private IEnumerator IceTrapActivation()
    {
        gameObject.transform.Find("Ice Trap 1").gameObject.SetActive(true);
        for (float i = 0; i <= 1f; i += Time.deltaTime*2f)
        {
            print(i);
            gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale = new Vector3(gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale.x
                                                                                                , i
                                                                                                , gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale.z);
            yield return new WaitForFixedUpdate();
        }
    }

    [PunRPC]
    void IceTrapDeActivation()
    {
        gameObject.transform.Find("Ice Trap 1").gameObject.SetActive(false);
    }

    //[PunRPC]
    //private void Destroy(int ID)
    //{
    //    PhotonNetwork.Destroy(PhotonView.Find(ID).gameObject);
    //}

    [PunRPC]
    private void Coroutine()
    {
        if(view.IsMine)
        {
            StartCoroutine(IceEffect());
        }
    }

    [PunRPC]
    private void TParam(float num)
    {
        if (view.IsMine)
        {
            curveFollow.tParam = num;
        }
    }

    [PunRPC]
    private void CrackIce(float iceSize)
    {
        if (view.IsMine)
        {
            gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale = new Vector3(gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale.x
                                                                                                , iceScale * iceSize
                                                                                                , gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale.z);

            print(gameObject.transform.Find("Ice Trap 1").gameObject.transform.localScale);
        }
    }
}
        
