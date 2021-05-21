using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowM : Enemies
{
    public Vector3 pos_initialPosition;
    public float distanceToPlayer;
    public bool canChase;
    public float agroRange;
    public float chaseRange;

    public Transform pos_lookAt;
    public float rotationSpeed;
    public Vector3 pos_lookDirection;

    public float deniedTimer=0f;
    public float deniedDuration;
    //public bool isDenied;

    public Renderer warningRenderer;
    public float blinkTime;
    public float maxBlinkTime;
    public bool blink;
    // Start is called before the first frame update
    void Start()
    {
        isDenied = false;
        pAgent = GetComponent<NavMeshAgent>();
        canChase = false;
        pos_initialPosition = transform.position;
        blink = false;
        warningRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(obj_player.transform.position, transform.position);
        pos_lookAt = obj_player.transform;
        pos_lookDirection = pos_lookAt.position - transform.position;
    }
}
