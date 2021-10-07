using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabBox : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction interactAction;
    public GameObject boxDetected;
    public GameObject boxGrabbed;
    public Transform grabPos;
    public bool grabingBox;
    CharacterController cc;
    PlayerMovement pm;
    float initialSpeed;
    float initialRotationSpeed;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        cc = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMovement>();
        initialSpeed = pm.movementSpeed;
        initialRotationSpeed = pm.turnSmoothTime;
    }

    // Update is called once per frame
    void Update()
    {
        DetectBox();
        BlockJumpingAndDC();
        MyInput();
        BoxController();
    }
    private void MyInput()
    { 
        if (interactAction.triggered&&PlayerSingleton.Instance.isGrounded)
        {
            if (boxGrabbed != null)
                LetBox();
            else if(boxDetected!=null)
                Grab();
        }
    }
    private void BoxController()
    {
        if (grabingBox && boxDetected != null)
        {
            boxDetected.transform.position = grabPos.position;
        }
    }
    private void Grab()
    {
        grabingBox = true;
        boxGrabbed = boxDetected;
        BoxIdentifier(boxGrabbed.GetComponent<BoxIdentifier>().boxType);
        Vector3 rotation = new Vector3(0, 0, 0);
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = true;
        boxGrabbed.transform.position = grabPos.position;
        boxGrabbed.transform.parent = grabPos;
        boxGrabbed.transform.localEulerAngles = rotation;
        
    }
    private void BoxIdentifier(int box)
    {
        switch (box)
        {
            case 0:
                cc.center = new Vector3(0, .14f, 1.51f);
                cc.radius = 1.47f;
                cc.height = 1.28f;
                pm.movementSpeed = .7f;
                pm.turnSmoothTime = .25f;
                break; 
            case 1:
                cc.center = new Vector3(0, .56f, 1.34f);
                cc.radius = 1.55f;
                cc.height = 2.1f;
                pm.movementSpeed = .65f;
                pm.turnSmoothTime = .3f;
                break; 
            case 2:
                cc.center = new Vector3(0, .84f, 1.35f);
                cc.radius = 1.55f;
                cc.height = 2.7f;
                pm.movementSpeed = .6f;
                pm.turnSmoothTime = .35f;
                break;
        }
    }
    private void LetBox()
    {
        cc.center = Vector3.zero;
        cc.radius = .5f;
        cc.height = 1;
        pm.turnSmoothTime = initialRotationSpeed;
        pm.movementSpeed = initialSpeed;
        grabingBox = false;
        boxGrabbed.GetComponent<Rigidbody>().useGravity = true;
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        boxGrabbed.transform.parent = null;
        boxGrabbed = null;

    }
    private void BlockJumpingAndDC()
    {
        if (grabingBox)
        {
            PlayerSingleton.Instance.canJump = false;
            PlayerSingleton.Instance.canUseDreamCatcher = false;
        }
        else
        {
            PlayerSingleton.Instance.canJump = true;
            PlayerSingleton.Instance.canUseDreamCatcher = true;
        }

    }
    void DetectBox()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, .1f))
        {
            if (hit.collider.CompareTag("Box"))
                boxDetected = hit.collider.gameObject;
            else
                boxDetected = null;
        }
        else
            boxDetected = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grabPos.position, .02f);
    }

}
