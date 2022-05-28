using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class collectibles : MonoBehaviour
{
    public static bool fireSpell = false, iceSpell = false, shieldSpell = false;
    [SerializeField] public List<GameObject> imageList;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (view.IsMine)
        {
            if (!fireSpell && !iceSpell && !shieldSpell)
            {

                if (other.gameObject.CompareTag("Fire Potion"))
                {
                    Destroy(other.gameObject);
                    fireSpell = true;
                    imageList[0].SetActive(true);
                }
                else if (other.gameObject.CompareTag("Ice Potion"))
                {
                    Destroy(other.gameObject);
                    iceSpell = true;
                    imageList[1].SetActive(true);
                }
                else if (other.gameObject.CompareTag("Shield Potion"))
                {
                    Destroy(other.gameObject);
                    shieldSpell = true;
                    imageList[2].SetActive(true);
                }
            }
        }
    }
}
