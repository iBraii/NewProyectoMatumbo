using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBox : MonoBehaviour
{
    public GameObject box;
    private PlayerInput _playerInput;
    private InputAction _interactAction;

    public bool grabingBox;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _interactAction = _playerInput.actions["Interact"];
        _interactAction.performed += Grab;
    }
    private void OnDisable()
    {
        _interactAction.performed -= Grab;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxMovement();
       
    }

    public void Grab(InputAction.CallbackContext context)
    {
        if (box != null)
        {
            if (grabingBox)
            {
                grabingBox = false;
                PlayerSingleton.Instance.canRotate = true;
                
            }else
            {
                grabingBox = true;
                box.GetComponent<ActualBox>().GetInitialDistanceToPlayer(transform);
                PlayerSingleton.Instance.canRotate = false;
            }
                
        }
        
    }

    public void BoxMovement()
    {
        if (grabingBox&&box!=null)
        {
            box.GetComponent<ActualBox>().Motion(GetComponent<PlayerMovement>().moveDirection);

        }
        else
        {
            grabingBox = false;
            PlayerSingleton.Instance.canRotate = true;
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
            box = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box"&&box!=null)
        {
            box = null;
        }
    }
}
