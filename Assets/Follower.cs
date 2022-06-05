using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

   


    [SerializeField] GameObject path;
    public float maxSteerAngle = 45000000000f;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 500f;
    public float currentSpeed ;
    public float maxSpeed = 100f;

    private List<Transform> nodes;
    private int currentNode = 0;

    private void Start()
    {
        
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
         nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
                
                
            }
        }

    }
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude)*90f ;
        //float newSteer = Mathf.Asin(relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        //print(newSteer);
    }
    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else { currentNode++;Debug.Log("girdi"); }
            
        }else { Debug.Log(Vector3.Distance(transform.position, nodes[currentNode].position)); }
    }
}
