using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class collectibles : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    public static int potionNum = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            Debug.Log("asdasdasd");
            potionNum++;
            text.text = potionNum.ToString();
        }
    }
}
