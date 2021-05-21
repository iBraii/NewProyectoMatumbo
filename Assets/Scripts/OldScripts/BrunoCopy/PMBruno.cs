using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMBruno : MonoBehaviour
{
    //--------Movement Vars---------------
    public CharacterController cmp_controller;
    public float speed=5;
    public float rotationSpeed=1;
    public float turnTime=1;
    public Transform pos_cam;
    public bool canMove = true;

    //---------Jump Vars------------------
    public Vector3 pos_playerVelocity;
    public float gravity=-9.81f;
    public float jumpForce=2f;
    public bool grounded;
    public float jumpRaycastDistance;
    public LayerMask whatIsGround;

    //---------Stress Vars----------------
    public float stress=0;
    public float maxStress=10f;
    public float stressCooldown;

    

    void Start()
    {
        cmp_controller = GetComponent<CharacterController>();
        pos_playerVelocity = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
