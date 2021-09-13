using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectM : Enemies
{
    
    public float distanceToPlayer;
    public GameObject[] form;

    public float agroRange;
    public float screamRange;
    public float timeToIddle;
    public float maxTimetoIddle;

    //public bool isDenied;
    public float deniedTimer;
    public float deniedDuration;

    public Renderer warningRenderer;
    public float blinkTime;
    public float maxBlinkTime;
    public bool blink;
    public GameObject player;
    public Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isDenied = false;
        isIddle = true;
        form[0].SetActive(true);
        form[1].SetActive(false);
        blink = false;
        warningRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, obj_player.transform.position);
        

       
    }
}
