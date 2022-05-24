using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    [SerializeField] public static int pos = 0;            // -1: left, 0: middle(default); 1:right
    int sign;               // Direction of movement
    public int speed = 1;   // Forward speed multiplayer
    int LerpRatio = 45, 
        LerpCount = 0;      // For Lerp operation 
    

    [SerializeField] GameObject fire;

    void Start()
    {
       
    }

    void Update()
    {
        // Determines which line the player swipe
        if (LerpCount == 0) {
            if (Input.GetKeyDown(KeyCode.A) && pos > -1)
            {
                pos--;
                sign = -1;              
            }
            else if (Input.GetKeyDown(KeyCode.D) && pos < 1)
            {
                pos++;
                sign = 1;             
            }
        }
        
        // Firing
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (collectibles.potionNum > 0) 
        //    {
        //        Rigidbody rb = Instantiate(fire, transform.position + transform.forward, Quaternion.identity).GetComponent<Rigidbody>();
        //        collectibles.potionNum--;
        //    }
        //    
        //}
    }
}
