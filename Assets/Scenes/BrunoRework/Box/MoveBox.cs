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
    void FixedUpdate()
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
                LookAtBox();
                PlayerSingleton.Instance.canRotate = false;
            }
                
        }
        
    }
    private void LookAtBox()
    {
        Vector3 lookAtBox = box.transform.position;
        lookAtBox.y = transform.position.y;
        transform.LookAt(lookAtBox);
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

    private void OnDrawGizmos()
    {
        if(box!=null)
        Debug.DrawRay(transform.position, (box.transform.position - transform.position), Color.red);
    }
}
