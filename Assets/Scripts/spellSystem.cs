using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellSystem : MonoBehaviour
{
    collectibles collectibles;
    [SerializeField] List<GameObject> speellList;
    [SerializeField] GameObject Shield;
    // Start is called before the first frame update
    void Start()
    {
        collectibles = GameObject.Find("Witch(Clone)").GetComponent<collectibles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && collectibles.fireSpell)
        {
            Debug.Log("Ateþ aktif");
            Instantiate(speellList[0], transform.position + transform.forward + transform.up, Quaternion.identity);
            collectibles.fireSpell = false;
            collectibles.imageList[0].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && collectibles.iceSpell)
        {
            Debug.Log("Buz aktif");
            Instantiate(speellList[1], transform.position + transform.forward + transform.up, Quaternion.identity);
            collectibles.iceSpell = false;
            collectibles.imageList[1].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && collectibles.shieldSpell)
        {
            Debug.Log("Kalkan aktif");
            Shield.SetActive(true);
            //Instantiate(speellList[2], transform.position + transform.forward + transform.up, Quaternion.identity);
            collectibles.shieldSpell = false;
            collectibles.imageList[2].SetActive(false);
        }
    }
}
