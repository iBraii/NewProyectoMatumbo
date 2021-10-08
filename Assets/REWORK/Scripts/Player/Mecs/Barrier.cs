using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool colisionDetected;
    public float distance;
    //public Vector3 savePosition;
    //public Transform origin;
    //public float distanceToOrigin;
    //public float initialDistanceToOrigin;
    public LayerMask lm;
    //void Start()
    //{
    //    initialDistanceToOrigin = Vector3.Distance(transform.position, origin.position);
    //}

    //void Update()
    //{
    //    distanceToOrigin = Vector3.Distance(transform.position, origin.position);
    //    if (colisionDetected)
    //        transform.position -= transform.forward * Time.deltaTime;
    //    else if (!colisionDetected && distanceToOrigin < initialDistanceToOrigin)
    //        transform.position += transform.forward * Time.deltaTime;
    //}

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * distance);
        colisionDetected = Physics.Raycast(transform.position, transform.forward, distance, lm);
    }
}
