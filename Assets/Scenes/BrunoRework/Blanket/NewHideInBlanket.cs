using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewHideInBlanket : MonoBehaviour
{

    [SerializeField] bool blanketAtReach;
    private PlayerInput playerInput;
    private InputAction interactAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }
    private void Update()
    {
        Hide();
    }
    void Hide()
    {
        if (blanketAtReach&&interactAction.triggered)
        {
            if (PlayerSingleton.Instance.isHiding)
            {
                PlayerSingleton.Instance.isHiding = false;
                Debug.Log("No hiding");
            }
            else
            {
                PlayerSingleton.Instance.isHiding = true;
                Debug.Log("Is Hiding");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blanket")
        {
            blanketAtReach = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Blanket")
        {
            blanketAtReach = false;
        }
    }
}
