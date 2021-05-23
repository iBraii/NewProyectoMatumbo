using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Movement))]
public class PlayerV : MonoBehaviour
{
    private PlayerC sc_playerC;
    private PlayerM sc_playerM;
    private Movement sc_movement;
    // Start is called before the first frame update
    void Awake()
    {
        sc_playerC = GetComponent<PlayerC>();
        sc_playerM = GetComponent<PlayerM>();
        sc_movement = GetComponent<Movement>();
        
    }
    //private void FixedUpdate()
    //{
    //    if (sc_playerC.obj_Box != null)
    //    {

    //        sc_playerC.BoxPush();

    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        sc_playerC.HideOnBlanket();
        sc_playerC.DreamCatcher();
        sc_playerC.StoryHintsControl();
        sc_playerM.LifeController(10);
        sc_playerC.Die();
        if (sc_playerM.canMove && sc_playerM.isUsingWeapon == false && sc_playerM.isHiding == false)
        {
            sc_movement.PlayerMovement(sc_playerM.speed, sc_playerM.rotationSpeed, sc_playerM.turnTime, sc_playerM.pos_cam);
        }
        if (sc_playerM.isHiding == false)
        {
            sc_playerC.PlayerJump();
        }
        if (sc_playerC.obj_Box != null)
        {

            sc_playerC.BoxPush();
            sc_playerC.BoxPull();
        }

    }
    //--------------------Triggers-------------------------------------
    private void OnTriggerEnter(Collider other)
    {

        sc_playerC.PickDC(other);
        sc_playerC.IsOnBlanket(other);
        sc_playerC.PickSH(other);
        sc_playerC.PickSweets(other);
        sc_playerC.DetectBox(other);

    }
    private void OnTriggerStay(Collider other)
    {
        sc_playerC.PickDC(other);
        sc_playerC.IsOnBlanket(other);
        sc_playerC.PickSH(other);
        sc_playerC.DetectBox(other);
    }

    private void OnTriggerExit(Collider other)
    {
        sc_playerC.IsOffBlanket(other);
        if (other.gameObject.CompareTag("Box"))
        {
            sc_playerC.obj_Box = null;
        }
    }
    
}
