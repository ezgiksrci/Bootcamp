using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bir þey çarptý!");
        if (collision.gameObject.CompareTag("Fire Spell") || collision.gameObject.CompareTag("Ice Spell") || collision.gameObject.CompareTag("Shield Spell"))
        {
            Destroy(collision.gameObject);
        }
    }
}
