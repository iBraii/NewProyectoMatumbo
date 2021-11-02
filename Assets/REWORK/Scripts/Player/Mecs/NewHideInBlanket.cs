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

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }
    private void Update()
    {
        Hide();
        if(unhide) Unhide();
    }
    private void Hide()
    {
        if (blanketAtReach&&interactAction.triggered)
        {
            if (PlayerSingleton.Instance.isHiding)
                unhide = true;
            else
            {
                PlayerSingleton.Instance.isHiding = true;
                SoundManager.instance.Play("BlanketOn");
            }
        }
    }

    private void Unhide()
    {
        unhideTimer += Time.deltaTime;
        if(unhideTimer >= 3.23f)
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
