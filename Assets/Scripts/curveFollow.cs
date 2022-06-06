using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class curveFollow : MonoBehaviour
{
    private GameObject path;
    [SerializeField] public static int pos, posFrom;
    [SerializeField] public static float speedModifier;
    public static int routeToGo;
    public static float tParam, tParamNext;
    public static Vector3 objectPosition, objectPositionNext;
    public static bool coroutineAllowed, isSwiping;
    int LerpRatio = 45;
    float LerpCount = 0f;      // For Lerp operation

    private List<Transform> routes;
    PhotonView view;

    void Start()
    {
        path = GameObject.Find("PlayerRoutes");
        routes = new List<Transform>();

        for (int i = 0; i < path.transform.childCount; i++)
        {
            routes.Add(path.transform.GetChild(i));
        }

        pos = SpawnPlayers.pos;
        posFrom = pos;
        routeToGo = 0;
        tParam = 0f;
        tParamNext = 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;
        isSwiping = false;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute());
        }
    }

    public IEnumerator GoByTheRoute()
    {
        if (view.IsMine)
        {
            coroutineAllowed = false;

            while (tParam < 1)
            {
                Vector3 p0 = routes[routeToGo].transform.GetChild(0).position;
                Vector3 p1 = routes[routeToGo].transform.GetChild(1).position;
                Vector3 p2 = routes[routeToGo].transform.GetChild(2).position;
                Vector3 p3 = routes[routeToGo].transform.GetChild(3).position;

                // Karakterin o anki Bezier �ekli i�indeki konumunu ve bakmas� gereken noktay� hesaplar
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
                
                // Oyuncunun karakter konumunu de�i�tirip de�i�tirmedi�ine bakar.
                if (movement.pos != pos && !isSwiping && speedModifier > 0f)
                {
                    posFrom = pos;
                    pos = movement.pos;
                    isSwiping = true;
                }

                // E�er karakter solda olmas� gerekiyorsa
                if (pos == -1)
                {
                    // E�er karakter sola kayd�r�l�yorsa
                    if (isSwiping)
                    {
                        // Karakter kademeli sola kayd�r�l�yor
                        objectPosition = Vector3.Lerp(objectPosition, objectPosition + (transform.right * -4), ((float)LerpCount / LerpRatio));
                        objectPositionNext = Vector3.Lerp(objectPositionNext, objectPositionNext + (transform.right * -4), ((float)LerpCount / LerpRatio));
                        LerpCount += Time.deltaTime * 100;

                        // Kayd�r�lma i�lemi tamamland�ysa
                        if (LerpCount >= LerpRatio)
                        {
                            LerpCount = 0f;
                            isSwiping = false;
                            posFrom = pos;
                        }
                    }
                    // E�er karakter zaten soldaysa
                    else
                    {
                        objectPosition += transform.right * -4;
                        objectPositionNext += transform.right * -4;
                    }
                }

                // E�er karakter sa�da olmas� gerekiyorsa
                else if (pos == 1)
                {
                    // E�er karakter sa�a kayd�r�l�yorsa
                    if (isSwiping)
                    {
                        // Karakter kademeli sola kayd�r�l�yor
                        objectPosition = Vector3.Lerp(objectPosition, objectPosition + (transform.right * 4), (float)LerpCount / LerpRatio);
                        objectPositionNext = Vector3.Lerp(objectPositionNext, objectPositionNext + (transform.right * 4), (float)LerpCount / LerpRatio);
                        LerpCount += Time.deltaTime * 100;

                        // Kayd�r�lma i�lemi tamamland�ysa
                        if (LerpCount >= LerpRatio)
                        {
                            LerpCount = 0f;
                            isSwiping = false;
                            posFrom = pos;
                        }
                    }

                    // E�er karakter zaten sa�daysa
                    else
                    {
                        objectPosition += transform.right * 4;
                        objectPositionNext += transform.right * 4;
                    }
                }

                // Karakter ortada olmas� gerekiyorsa
                else if (pos == 0)
                {
                    // Karakter ortaya kayd�r�l�yorsa
                    if (isSwiping)
                    {
                        // Sa�dan ortaya ge�iyorsa
                        if (posFrom == 1)
                        {
                            objectPosition = Vector3.Lerp(objectPosition + (transform.right * 4), objectPosition, (float)LerpCount / LerpRatio);
                            objectPositionNext = Vector3.Lerp(objectPositionNext + (transform.right * 4), objectPositionNext, (float)LerpCount / LerpRatio);
                            LerpCount += Time.deltaTime * 100;

                            // Kayd�r�lma i�lemi tamamland�ysa
                            if (LerpCount >= LerpRatio)
                            {
                                LerpCount = 0f;
                                isSwiping = false;
                                posFrom = pos;
                            }
                        }

                        //Soldan ortaya ge�iyorsa
                        else if (posFrom == -1)
                        {
                            objectPosition = Vector3.Lerp(objectPosition + (transform.right * -4), objectPosition, (float)LerpCount / LerpRatio);
                            objectPositionNext = Vector3.Lerp(objectPositionNext + (transform.right * -4), objectPositionNext, (float)LerpCount / LerpRatio);
                            LerpCount += Time.deltaTime * 100;

                            // Kayd�r�lma i�lemi tamamland�ysa
                            if (LerpCount >= LerpRatio)
                            {
                                LerpCount = 0f;
                                isSwiping = false;
                                posFrom = pos;
                            }
                        }
                    }
                }

                // Hesaplanan karakter konumu ve bak��� i�leniyor
                transform.position = objectPosition;
                transform.LookAt(objectPositionNext);

                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;
            routeToGo += 1;

            if (routeToGo > routes.Count - 1)
            {
                routeToGo = 0;
            }

            coroutineAllowed = true;
        }
    }
}
