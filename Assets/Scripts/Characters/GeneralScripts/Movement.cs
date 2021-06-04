using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController cmp_controller;
    public Rigidbody rb;
    public PlayerC sc_playerC;
    private PlayerM sc_playerM;
    private GameObject player;
    float targetAngle;
    Vector3 moveDirection;

    //private float smoothVel;
    void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>() as CharacterController;
        //rb = GetComponent<Rigidbody>();
        cmp_controller = GetComponent<CharacterController>();
        sc_playerC = GetComponent<PlayerC>();
        sc_playerM = GetComponent<PlayerM>();
        player = GameObject.Find("Player");
        //smoothVel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMovement(float speed, float rotationSpeed,float turnTime, Transform pos_cam)
    {

        Vector3 direction = new Vector3(sc_playerC.horizontal, 0f, sc_playerC.vertical).normalized;


        if (direction.magnitude >= 0.1f && sc_playerM.isMovingBox == false)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + pos_cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, turnTime);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cmp_controller.Move(moveDirection * speed * Time.deltaTime);
        }
        else if(direction.magnitude >= 0.1f && sc_playerM.isMovingBox == true)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.transform.eulerAngles.y;
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cmp_controller.Move(moveDirection * speed * Time.deltaTime);
        }   
    }



    public void Move_Anydir(float speedx,float speedy,float speedz)
    {
        rb.velocity = new Vector3(speedx, speedy, speedz);
        //rb.AddForce(new Vector3(speedx, speedy, speedz),ForceMode.VelocityChange);
        //rb.MovePosition(new Vector3(0,0,0));
    }
    ///////////////////////////////////////////////////////////////////////////////////////
    //public void PlayerRotation(Vector3 move, float spd,Transform pos_cam)
    //{
    //    if (move.magnitude >= 0.1f)
    //    {
    //        float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + pos_cam.eulerAngles.y;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVel, 0.1f);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    //        rb.velocity = moveDirection * spd * Time.deltaTime;
    //    }
    //}

}
