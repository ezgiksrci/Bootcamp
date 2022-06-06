using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class curveFollowForPotion : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] private int pos;
    private int routeToGo;
    private float tParam, tParamNext;
    private Vector3 objectPosition, objectPositionNext;
    private float speedModifier;
    private bool coroutineAllowed;

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

        // Büyünün fýrlatýldýðý konumu çeker
        pos = curveFollow.pos;
        objectPosition = curveFollow.objectPosition;
        objectPositionNext = curveFollow.objectPositionNext;
        routeToGo = curveFollow.routeToGo;
        tParam = curveFollow.tParam + 0.075f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
        view = GetComponent<PhotonView>();

        StartCoroutine(DestroyAfterTenSecs());
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

            //Debug.Log(pos);
            if (pos == -1)
            {
                objectPosition += transform.right * -4;
                objectPositionNext += transform.right * -4;
            }
            else if (pos == 1)
            {
                objectPosition += transform.right * 4;
                objectPositionNext += transform.right * 4;
            }

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

    IEnumerator DestroyAfterTenSecs()
    {
        yield return new WaitForSeconds(10);
        PhotonNetwork.Destroy(gameObject);
    }
}
