using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVBruno : MonoBehaviour
{
    public PCBRuno controller;
    public PMBruno model;
    public Movement movement;
    public Image stressBar;
    void Start()
    {
        controller = GetComponent<PCBRuno>();
        model = GetComponent<PMBruno>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (model.canMove)
        {
            movement.PlayerMovement(model.speed, model.rotationSpeed, model.turnTime, model.pos_cam);
        }
       
        controller.PlayerJump();
        
        

        if (Input.GetMouseButtonDown(0))
        {
            controller.AddStress();
        }

        stressBar.fillAmount = model.stress / model.maxStress;        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candy"))
        {
            if (model.stress > 0)
            {
                controller.ReduceStress(2);
                other.gameObject.SetActive(false);
            }
        }
    }

    
}
