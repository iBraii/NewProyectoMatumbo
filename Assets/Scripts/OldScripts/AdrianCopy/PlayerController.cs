using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rb;
    public Transform cam;
    public float speed;
    public float turnTime = 0.5f;
    private float gravity =-9.81f;
    public bool grounded;
    float rotationSpeed;
    public float smoothVel;
    public Vector3 playerVelocity;
    public Vector3 rotatioProves;
    public float x;
    public float z;
    private PlayerModel sc_playerModel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        sc_playerModel = GetComponent<PlayerModel>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sc_playerModel.isUsingWeapon == false && sc_playerModel.isHiding == false)
        {
            Movement();
        }  
    }

    void Movement()
    {
         x = Input.GetAxisRaw("Horizontal");
         z = Input.GetAxisRaw("Vertical");



    Vector3 direction = new Vector3(x, 0f, z).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed,turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward;
            controller.Move(moveDirection*speed*Time.deltaTime);
        }
        grounded = controller.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Move_in_X(float spd_mov, int dir_mov)
    {
        rb.velocity = new Vector3(spd_mov * dir_mov * Time.deltaTime, rb.velocity.y,rb.velocity.z);
    }

    public void Move_in_Y(float spd_mov, int dir_mov)
    {
        rb.velocity = new Vector3(rb.velocity.x, spd_mov * dir_mov * Time.deltaTime, rb.velocity.z);
    }

    public void Move_in_Z(float spd_mov, int dir_mov)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, spd_mov * dir_mov * Time.deltaTime);
    }
    public void Rote_Y_Two(Vector3 move, float spd)
    {
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVel, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }
}
