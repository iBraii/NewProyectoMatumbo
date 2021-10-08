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
    PlayerMovement pm;
    float initialSpeed;
    float initialRotationSpeed;

    public GameObject barrierPref;
    public Transform barrierPos;
    public float distance;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
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
        Debug.DrawRay(boxGrabbed.transform.position, transform.forward * distance);
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
        if (PlayerSingleton.Instance.grabingBox && boxDetected != null)
        {
            boxDetected.transform.position = grabPos.position;
        }
    }
    private void Grab()
    {
        PlayerSingleton.Instance.grabingBox = true;
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

    }

    private void LetBox()
    {
        pm.turnSmoothTime = initialRotationSpeed;
        pm.movementSpeed = initialSpeed;
        PlayerSingleton.Instance.grabingBox = false;
        boxGrabbed.GetComponent<Rigidbody>().useGravity = true;
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        boxGrabbed.transform.parent = null;
        boxGrabbed = null;

    }
    private void BlockJumpingAndDC()
    {
        if (PlayerSingleton.Instance.grabingBox)
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
