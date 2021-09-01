using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CulpaM : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoint;
    public int waypointIndex;
    public float distanceToWaypoint;
    public GameObject thirdPersonCamera;
    public GameObject player;
    public GameObject aura;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        waypointIndex = 0;
        agent.SetDestination(waypoint[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
