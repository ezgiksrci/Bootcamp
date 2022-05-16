using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellSystem : MonoBehaviour
{
    collectibles collectibles;
    [SerializeField] List<GameObject> speellList;
    // Start is called before the first frame update
    void Start()
    {
        collectibles = GameObject.Find("Witch").GetComponent<collectibles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && collectibles.potionNum == 1)
        {
            Rigidbody rb = Instantiate(speellList[0], transform.position + Vector3.forward, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            collectibles.potionNum--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && collectibles.potionNum == 2)
        {
            Rigidbody rb = Instantiate(speellList[1], transform.position + Vector3.forward, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            collectibles.potionNum -=2;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && collectibles.potionNum == 3)
        {
            Debug.Log("Kalkan aktif");
            collectibles.potionNum -= 3;
        }
    }
}
