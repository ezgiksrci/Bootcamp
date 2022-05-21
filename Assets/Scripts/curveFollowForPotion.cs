using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curveFollowForPotion : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private int pos;
    private int routeToGo;
    private float tParam, tParamNext;
    private Vector3 objectPosition, objectPositionNext;
    private float speedModifier;
    private bool coroutineAllowed;

    void Start()
    {
        // Büyünün fýrlatýldýðý konumu çeker
        pos = curveFollow.pos;
        objectPosition = curveFollow.objectPosition;
        objectPositionNext = curveFollow.objectPositionNext;
        routeToGo = curveFollow.routeToGo;
        tParam = curveFollow.tParam;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
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

            Debug.Log(pos);
            if (pos == -1)
            {
                objectPosition += transform.right * -2;
                objectPositionNext += transform.right * -2;
            }
            else if (pos == 1)
            {
                objectPosition += transform.right * 2;
                objectPositionNext += transform.right * 2;
            }

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
