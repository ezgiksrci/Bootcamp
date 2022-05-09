using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class collectibles : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    public static int potionNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            potionNum++;
            text.text = potionNum.ToString();
        }
    }
}
