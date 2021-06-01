using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController cmp_controller;
    public Rigidbody rb;
    public PlayerC sc_playerC;
    void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>() as CharacterController;
        cmp_controller = GetComponent<CharacterController>();
        sc_playerC = GetComponent<PlayerC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMovement(float speed, float rotationSpeed,float turnTime, Transform pos_cam)
    {


        if (Input.GetKey(sc_playerC.keyUp))
        {
            sc_playerC.vertical = 1;
        }
        else if (Input.GetKey(sc_playerC.keyDown))
        {
            sc_playerC.vertical = -1;
        }
        else if (!Input.GetKey(sc_playerC.keyDown) && !Input.GetKey(sc_playerC.keyUp))
        {
            sc_playerC.vertical = 0;
        }
        if (Input.GetKey(sc_playerC.keyRight))
        {
            sc_playerC.horizontal = 1;
        }
        else if (Input.GetKey(sc_playerC.keyLeft))
        {
            sc_playerC.horizontal = -1;
        }
        else if (!Input.GetKey(sc_playerC.keyLeft) && !Input.GetKey(sc_playerC.keyRight))
        {
            sc_playerC.horizontal = 0;
        }
        if (Input.GetKey(sc_playerC.keyDown) && Input.GetKey(sc_playerC.keyUp))
        {
            sc_playerC.vertical = 0;
        }
        if (Input.GetKey(sc_playerC.keyLeft) && Input.GetKey(sc_playerC.keyRight))
        {
            sc_playerC.horizontal = 0;
        }

        Vector3 direction = new Vector3(sc_playerC.horizontal, 0f, sc_playerC.vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + pos_cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, turnTime);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cmp_controller.Move(moveDirection * speed * Time.deltaTime);
        }

       
    }
    public void PlayerRotation()
    {

    }
    public void Move_Anydir(float speedx,float speedy,float speedz)
    {
        rb.velocity = new Vector3(speedx, speedy, speedz);
        //rb.AddForce(new Vector3(speedx, speedy, speedz),ForceMode.VelocityChange);
        //rb.MovePosition(new Vector3(0,0,0));
    }

}
