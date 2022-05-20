using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cellat geldi!!!");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Cellat geldi2");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
