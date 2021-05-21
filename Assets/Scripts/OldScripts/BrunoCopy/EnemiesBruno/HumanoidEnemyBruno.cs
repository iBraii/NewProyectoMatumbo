using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidEnemyBruno : EnemiesBruno
{
    public bool isAttacking;
    public bool isFollowingPlayer;

    public NavMeshAgent agent;
    public Transform[] waypoint;
    public int waypointIndex;
    [SerializeField]
    private float distanceToWaypoint;
    
    private void Awake()
    {
        
    }
    void Start()
    {
        waypointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        
        agent.SetDestination(waypoint[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        CloseToWaypoint();
    }
    private void Attack()
    {

    }
    private void FollowPlayer()
    {

    }
    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoint.Length)
            waypointIndex = 0;
    }
    void CloseToWaypoint()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, waypoint[waypointIndex].position);
        if (distanceToWaypoint <= 1)
        {
            IncreaseIndex();
            agent.SetDestination(waypoint[waypointIndex].position);
        }

    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoint.Length; i++)
        {
            Gizmos.DrawCube(waypoint[i].position, Vector3.one/2);
        }

    }
}
