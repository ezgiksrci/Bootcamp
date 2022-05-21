using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
    int posOffset = 1;      // Amount of movement when character switches
=======
    int posOffset = 2;      // Amount of movement when character switches
>>>>>>> parent of 117693f (asd)
    int pos = 0;            // -1: left, 0: middle(default); 1:right
=======
    
    [SerializeField] public static int pos = 0;            // -1: left, 0: middle(default); 1:right
>>>>>>> 9d1028fff03698b8c41cd6d1c422aec5c2fce00a
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collectibles.potionNum > 0) 
            {
                Rigidbody rb = Instantiate(fire, transform.position + transform.forward, Quaternion.identity).GetComponent<Rigidbody>();
                collectibles.potionNum--;
            }
            
        }
    }
}
