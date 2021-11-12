using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class NewHideInBlanket : MonoBehaviour
{

    private bool blanketAtReach;
    private PlayerInput playerInput;
    private InputAction interactAction;
    [HideInInspector] public bool unhide;
    private float unhideTimer;

    //Referencia a clase
    private GrabBox gb;

    //Debug 
    public bool attacked;
    public float stress;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        gb = GetComponent<GrabBox>();
    }
    private void Update()
    {
        Hide();
        if(unhide) Unhide();
        attacked = PlayerSingleton.Instance.beingAttacked;
        stress = PlayerSingleton.Instance.stress;
    }
    private void Hide()
    {
        if (PlayerSingleton.Instance.onAnimation||gb.boxDetected!=null) return;
        if (blanketAtReach&&interactAction.triggered)
        {
            if (PlayerSingleton.Instance.isHiding)
                unhide = true;
            else
            {
                PlayerSingleton.Instance.isHiding = true;
                PlayerSingleton.Instance.beingAttacked = false;
                SoundManager.instance.Play("BlanketOn");
            }
        }
    }

    private void Unhide()
    {
        unhideTimer += Time.deltaTime;
        if(unhideTimer >= 3.23f/2)
        {
            unhideTimer = 0;
            PlayerSingleton.Instance.isHiding = false;
            SoundManager.instance.Play("BlanketOff");
            unhide = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blanket") blanketAtReach = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Blanket") blanketAtReach = false;
    }
}
