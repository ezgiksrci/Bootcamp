using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opponentMove : MonoBehaviour
{
    private int posOffset = 2;      // Amount of movement when character switches
    private int pos = 0;            // -1: left, 0: middle(default); 1:right
    private int sign;               // Direction of movement
    private int LerpRatio = 45;
    private int LerpCount = 0;      // For Lerp operation 
    private float currPosX;         // Holds X position before lerping
    public int speed = 1;           // Forward speed multiplayer

    private Animator anim;
    private Vector3 charPos;
    private Rigidbody r3D;
    void Start()
    {
        charPos = transform.position;
        anim = GetComponent<Animator>();
        r3D = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Determines which line the player swipe
        if (LerpCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && pos > -1)
            {
                pos--;
                sign = -1;
                currPosX = r3D.position.x;
                LerpCount++;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && pos < 1)
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
            //Debug.Log(LerpCount);
            charPos = new Vector3(Mathf.Lerp(currPosX, currPosX + posOffset * sign, (float)LerpCount / LerpRatio), r3D.position.y, r3D.position.z);
            r3D.position = charPos;
            LerpCount++;
            if (LerpCount == LerpRatio)
            {
                currPosX = r3D.position.x;
                LerpCount = 0;
            }
        }

        // Forward position update
        charPos = new Vector3(r3D.position.x, (float)(r3D.position.y + Mathf.Sin(r3D.position.z) * 0.00015), r3D.position.z + speed * Time.deltaTime);
        r3D.position = charPos;
        //Debug.Log(r3D.position.z);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire Spell"))

        {
            StartCoroutine(FireEffect());
        }
        else if (other.CompareTag("Ice Spell"))
        {
            StartCoroutine(IceEffect());
        }
    }

    IEnumerator FireEffect()
    {
        speed = 5;
        yield return new WaitForSeconds(3f);
        speed = 10;
    }

    IEnumerator IceEffect()
    {
        speed = 0;
        yield return new WaitForSeconds(3f);
        speed = 10;
    }
}

