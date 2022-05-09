using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    int posOffset = 2;      // Amount of movement when character switches
    int pos = 0;            // -1: left, 0: middle(default); 1:right
    int sign;               // Direction of movement
    public int speed = 1;   // Forward speed multiplayer
    int LerpRatio = 45, 
        LerpCount = 0;      // For Lerp operation 
    float currPosX;         // Holds X position before lerping

    [SerializeField] GameObject fire;

    Vector3 charPos;
    public Animator anim;
    public Rigidbody r3D;
    void Start()
    {
        charPos = transform.position;
        anim = GetComponent<Animator>();
        r3D = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Determines which line the player swipe
        if (LerpCount == 0) {
            if (Input.GetKeyDown(KeyCode.A) && pos > -1)
            {
                pos--;
                sign = -1;
                currPosX = r3D.position.x;
                LerpCount++;
            }
            else if (Input.GetKeyDown(KeyCode.D) && pos < 1)
            {
                pos++;
                sign = 1;
                currPosX = r3D.position.x;
                LerpCount++;
            }
        }
        
        // Lerping while swipe
        if (LerpCount > 0)
        {
            Debug.Log(LerpCount);
            charPos = new Vector3(Mathf.Lerp(currPosX, currPosX + posOffset*sign, (float)LerpCount/LerpRatio), r3D.position.y, r3D.position.z);
            r3D.position = charPos;
            LerpCount++;
            if (LerpCount == LerpRatio)
            {
                currPosX = r3D.position.x;
                LerpCount = 0;
            }
        }
        
        // Forward position update
        charPos = new Vector3(r3D.position.x, (float)(r3D.position.y + Mathf.Sin(r3D.position.z)*0.00015), r3D.position.z + speed * Time.deltaTime);
        r3D.position = charPos;
        Debug.Log(r3D.position.z);

        // Firing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collectibles.potionNum > 0) 
            {
                Rigidbody rb = Instantiate(fire, transform.position + Vector3.forward, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
                collectibles.potionNum--;
            }
            
        }
    }
}
