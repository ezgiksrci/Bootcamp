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
                Destroy(other.gameObject);
                fireSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (other.gameObject.CompareTag("Ice Potion"))
            {
                Destroy(other.gameObject);
                iceSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (other.gameObject.CompareTag("Shield Potion"))
            {
                Destroy(other.gameObject);
                shieldSpell = true;
                gameObject.transform.Find("Canvas").gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}
