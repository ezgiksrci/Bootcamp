using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!! Multiplayer özelliði geldiði zaman deðiþtirilecek !!!

public class opponentMove : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] public static int pos, posFrom;
    [SerializeField] private float speedModifier;
    [SerializeField] GameObject iceTrap;
    
    public static int routeToGo;
    public static float tParam, tParamNext;
    public static Vector3 objectPosition, objectPositionNext;
    private bool coroutineAllowed, isSwiping;
    int LerpRatio = 45,
        LerpCount = 0;      // For Lerp operation

    void Start()
    {
        pos = 0;
        posFrom = pos;
        routeToGo = 1;
        tParam = 0f;
        tParamNext = 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;
        isSwiping = false;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice Spell"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(IceEffect());
        }
    }

    private IEnumerator IceEffect()
    {
        iceTrap.SetActive(true);
        speedModifier = 0;
        yield return new WaitForSeconds(5);
        speedModifier = 0.25f;
        iceTrap.SetActive(false);
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNum].GetChild(0).position;
        Vector3 p1 = routes[routeNum].GetChild(1).position;
        Vector3 p2 = routes[routeNum].GetChild(2).position;
        Vector3 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            // Karakterin o anki Bezier Þekli içindeki konumunu ve bakmasý gereken noktayý hesaplar
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 
                            + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 
                            + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 
                            + Mathf.Pow(tParam, 3) * p3;
            
            tParamNext = tParam + Time.deltaTime * speedModifier;

            objectPositionNext = Mathf.Pow(1 - tParamNext, 3) * p0
                            + 3 * Mathf.Pow(1 - tParamNext, 2) * tParamNext * p1
                            + 3 * (1 - tParamNext) * Mathf.Pow(tParamNext, 2) * p2
                            + Mathf.Pow(tParamNext, 3) * p3;

            // Oyuncunun karakter konumunu deðiþtirip deðiþtirmediðine bakar.
            //if (movement.pos != pos && !isSwiping)
            //{
            //    posFrom = pos;
            //    pos = movement.pos;
            //    isSwiping = true;
            //}

            // Eðer karakter solda olmasý gerekiyorsa
            if (pos == -1)
            {
                // Eðer karakter sola kaydýrýlýyorsa
                if (isSwiping)
                {   
                    // Karakter kademeli sola kaydýrýlýyor
                    objectPosition = Vector3.Lerp(objectPosition, objectPosition + (transform.right * -2), (float)LerpCount/LerpRatio);
                    objectPositionNext = Vector3.Lerp(objectPositionNext, objectPositionNext + (transform.right * -2), (float)LerpCount / LerpRatio);
                    LerpCount++;

                    // Kaydýrýlma iþlemi tamamlandýysa
                    if(LerpCount == LerpRatio)
                    {
                        LerpCount = 0;
                        isSwiping = false;
                        posFrom = pos;
                    }
                }
                // Eðer karakter zaten soldaysa
                else 
                {
                    objectPosition += transform.right * -2;
                    objectPositionNext += transform.right * -2; 
                }
                
            }

            // Eðer karakter saðda olmasý gerekiyorsa
            else if (pos == 1)
            {
                // Eðer karakter saða kaydýrýlýyorsa
                if (isSwiping)
                {
                    // Karakter kademeli sola kaydýrýlýyor
                    objectPosition = Vector3.Lerp(objectPosition, objectPosition + (transform.right * 2), (float)LerpCount / LerpRatio);
                    objectPositionNext = Vector3.Lerp(objectPositionNext, objectPositionNext + (transform.right * 2), (float)LerpCount / LerpRatio);
                    LerpCount++;

                    // Kaydýrýlma iþlemi tamamlandýysa
                    if (LerpCount == LerpRatio)
                    {
                        LerpCount = 0;
                        isSwiping = false;
                        posFrom = pos;
                    }
                }

                // Eðer karakter zaten saðdaysa
                else
                {
                    objectPosition += transform.right * 2;
                    objectPositionNext += transform.right * 2;
                }
                
            }

            // Karakter ortada olmasý gerekiyorsa
            else if (pos == 0)
            {
                // Karakter ortaya kaydýrýlýyorsa
                if (isSwiping)
                {
                    // Saðdan ortaya geçiyorsa
                    if(posFrom == 1)
                    {
                        objectPosition = Vector3.Lerp(objectPosition + (transform.right * 2), objectPosition, (float)LerpCount / LerpRatio);
                        objectPositionNext = Vector3.Lerp(objectPositionNext + (transform.right * 2), objectPositionNext, (float)LerpCount / LerpRatio);
                        LerpCount++;

                        // Kaydýrýlma iþlemi tamamlandýysa
                        if (LerpCount == LerpRatio)
                        {
                            LerpCount = 0;
                            isSwiping = false;
                            posFrom = pos;
                        }
                    }

                    //Soldan ortaya geçiyorsa
                    else if (posFrom == -1)
                    {
                        objectPosition = Vector3.Lerp(objectPosition + (transform.right * -2), objectPosition, (float)LerpCount / LerpRatio);
                        objectPositionNext = Vector3.Lerp(objectPositionNext + (transform.right * -2), objectPositionNext, (float)LerpCount / LerpRatio);
                        LerpCount++;

                        // Kaydýrýlma iþlemi tamamlandýysa
                        if (LerpCount == LerpRatio)
                        {
                            LerpCount = 0;
                            isSwiping = false;
                            posFrom = pos;
                        }
                    }
                }
            }
            
            // Hesaplanan karakter konumu ve bakýþý iþleniyor
            transform.position = objectPosition;
            transform.LookAt(objectPositionNext);

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }
}

