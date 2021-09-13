using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspectroCulpaNuevo : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoint;

    [Header("Variables")]
    private float speed;
    public float normalSpeed;
    public float fastSpeed;
    public float slowSpeed;
    public float rotationSpeed;
    private int currentIndex;
    private float rotationTime;
    

    [Header("Dificultad Dinamica")]
    public float minTreshhold;
    public float maxTreshhold;
    private void Start()
    {
        currentIndex = 0;
        speed = normalSpeed;
    }
    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        rotationTime += Time.deltaTime*rotationSpeed;
        Quaternion lookRotation = Quaternion.LookRotation(waypoint[currentIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationTime);
        transform.position = Vector3.MoveTowards(transform.position,waypoint[currentIndex].position , speed*Time.deltaTime);    
        Mathf.Clamp(rotationTime, 0, 1);

        if (transform.position == waypoint[currentIndex].position)
        {
            rotationTime = 0;
            CheckIfCompleted();
                      
        }
    }
    private void CheckIfCompleted()
    {
        if (currentIndex == waypoint.Length - 1)
        {
            Deactivate();
        }
        else
        {
            SpeedController();
            currentIndex++;
        }
    }
    private void SpeedController()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.Find("NewPlayer").transform.position);
        if (distanceToPlayer < minTreshhold)
            speed = slowSpeed;
        else if (distanceToPlayer > minTreshhold && distanceToPlayer < maxTreshhold)
            speed = normalSpeed;
        else if (distanceToPlayer > maxTreshhold)
            speed = fastSpeed;

        Debug.Log("Speed: " + speed);
        Debug.Log("Distance: " + distanceToPlayer);
    }
    private void Deactivate()
    {
        currentIndex = 0;
    }
    private void OnDrawGizmos()
    {
        foreach(Transform waypoint in waypoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoint.position, .3f);
        }

        for(int i = 0; i < waypoint.Length - 1; i++)
        {
            Debug.DrawLine(waypoint[i].position, waypoint[i + 1].position, Color.red);
        }       
    }

}
