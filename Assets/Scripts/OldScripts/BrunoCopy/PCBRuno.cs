using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCBRuno : MonoBehaviour
{
    public CharacterController cmp_controller;
    public Movement movement;
    public PMBruno model;

    
    void Start()
    {
        cmp_controller = GetComponent<CharacterController>();
        movement = GetComponent<Movement>();
        model = GetComponent<PMBruno>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    public void AddStress()
    {
        if (model.stress < 10)
        {
            model.stress++;
        }
        model.stressCooldown = 0;
    }

    public void ReduceStress(float ammount)
    {
        model.stress -= ammount;
        if (model.stress < 0)
            model.stress = 0;
    }

    public void PlayerJump()
    {

        model.grounded = GrounCheck();
        model.pos_playerVelocity.y += model.gravity * Time.deltaTime;

        if (model.grounded && model.pos_playerVelocity.y < 0)
        {
            model.pos_playerVelocity.y = 0f;
        }

        if (model.pos_playerVelocity.y > 0)
        {
            model.pos_playerVelocity += Vector3.up * model.gravity * Time.deltaTime;
        }
        cmp_controller.Move(model.pos_playerVelocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && model.grounded)
        {
            model.pos_playerVelocity.y += Mathf.Sqrt(model.jumpForce * -3f * model.gravity);
        }

    }

    bool GrounCheck()
    {
        Debug.DrawRay(transform.position, Vector3.down * model.jumpRaycastDistance, Color.red);

        return Physics.Raycast(transform.position, Vector3.down, model.jumpRaycastDistance,model.whatIsGround);
    }



}
