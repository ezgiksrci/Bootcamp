using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spellSystem : MonoBehaviour
{
    collectibles collectibles;
    [SerializeField] List<GameObject> speellList;
    [SerializeField] GameObject Shield;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curveFollow.speedModifier != 0)
        {
            if ((Input.GetTouch(0).tapCount == 2 || Input.GetKeyDown(KeyCode.Space)) && collectibles.fireSpell && view.IsMine)
            {
                Debug.Log("Ateþ aktif");
                PhotonNetwork.Instantiate(speellList[0].name, transform.position + transform.forward * 2, Quaternion.identity);
                collectibles.fireSpell = false;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if ((Input.GetTouch(0).tapCount == 2 || Input.GetKeyDown(KeyCode.Space)) && collectibles.iceSpell && view.IsMine)
            {
                Debug.Log("Buz aktif");
                PhotonNetwork.Instantiate(speellList[1].name, transform.position + transform.forward * 2, Quaternion.identity);
                collectibles.iceSpell = false;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if ((Input.GetTouch(0).tapCount == 2 || Input.GetKeyDown(KeyCode.Space)) && collectibles.shieldSpell && view.IsMine)
            {
                Debug.Log("Kalkan aktif");
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("ShieldSync", RpcTarget.All);
                collectibles.shieldSpell = false;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    [PunRPC]
    void ShieldSync()
    {
        gameObject.transform.Find("Shield").gameObject.SetActive(true);
    }
}
