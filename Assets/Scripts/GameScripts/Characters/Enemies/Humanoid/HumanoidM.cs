using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidM : Enemies
{
    public bool isAttacking;
    public bool isFollowingPlayer;
    public HumanoidC sc_humanoidC;
    //-----Variables para patrulla-----
    public Transform[] waypoint;
    public int waypointIndex;
    public float distanceToWaypoint;

    //----Variables para seguimiento-----
    public float agroRange;
    public float distanceToPlayer;
    public float timeToReturn;
    public float timeToReturnMax;
    public GameObject player;
    public Transform pos_lookAt;
    public float rotationSpeed;
    public Vector3 pos_lookDirection;
    //-----Detección feedback------------
    public Renderer warningRenderer;
    public float blinkTime;
    public float maxBlinkTime;
    public bool blink;

    //------Rango de vision--------
    public bool playerOnView;
    [Range(0,360)]
    public float visionAngle;
    public float visionDistance;
    public float halfVisionAngle;
    //-----Variables para aturdimiento-----
    //public bool isDenied;
    public float stunTime;
    public float maxStunTime;

    // Start is called before the first frame update

    void Start()
    {
        waypointIndex = 0;
        player = GameObject.Find("Player");
        pAgent = GetComponent<NavMeshAgent>();
        pAgent.SetDestination(waypoint[waypointIndex].position);
        isFollowingPlayer = false;
        warningRenderer.enabled = false;
        blink = false;
        playerOnView = false;
    }


    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        pos_lookAt = player.transform;
        pos_lookDirection = pos_lookAt.position - transform.position;
    }

    

    
}
