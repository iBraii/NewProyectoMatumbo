using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class GrabBox : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction interactAction;
    [SerializeField] private GameObject boxDetected;
    [SerializeField] private GameObject boxGrabbed;
    private PlayerMovement pm;
    private CharacterController cc;
    private float initialSpeed;
    private float initialRotationSpeed;

    [SerializeField] [Tooltip("Box new position")] private Transform grabPos;

    [SerializeField] private float distanceFromBox;
    private bool boxAvailable;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        pm = GetComponent<PlayerMovement>();
        initialSpeed = pm.movementSpeed;
        initialRotationSpeed = pm.turnSmoothTime;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectBox();
        BlockJumpingAndDC();
        MyInput();
        //BoxController();
        Falling();
        BoxAvailable();
    }
    private void MyInput()
    { 
        if (interactAction.triggered && PlayerSingleton.Instance.isGrounded)
        {
            if (boxGrabbed != null)
                StartCoroutine(LetBox(.35f));
            else if (boxDetected != null/* && boxAvailable*/)
                StartCoroutine(Grab());
        }
    }
    private void BoxController()
    {
        if (PlayerSingleton.Instance.grabingBox && boxDetected != null)
            boxDetected.transform.position = grabPos.position;
    }
    System.Collections.IEnumerator Grab()
    {
        
        PlayerSingleton.Instance.grabingBox = true;
        PlayerSingleton.Instance.canMove = false;
        SoundManager.instance.Play("BoxLift");

        boxGrabbed = boxDetected;
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = true;       
       

        Vector3 rotation = new Vector3(0,boxGrabbed.transform.localEulerAngles.y,0);
        yield return new WaitForSeconds(1f);

        boxGrabbed.transform.parent = grabPos;
        boxGrabbed.transform.DOLocalMove(Vector3.zero, .4f, false);
        if(boxGrabbed.transform.localEulerAngles.x!=Vector3.zero.x|| boxGrabbed.transform.localEulerAngles.z != Vector3.zero.z)
            boxGrabbed.transform.DORotate(rotation, .27f, RotateMode.Fast);
        boxGrabbed.layer = 2;

        pm.useGravity = false;
        //.27
        BoxIdentifier(boxGrabbed.GetComponent<BoxIdentifier>().boxType);
        PlayerSingleton.Instance.canMove = true;
    }
    private void BoxIdentifier(int box)
    {
        switch (box)
        {
            case 0:
                pm.movementSpeed = .6f;
                pm.turnSmoothTime = .35f;

                cc.center = new Vector3(0, 0.1f, .16f);
                cc.radius = .11f;
                cc.height = .2f;
                break;  
            case 1:
                pm.movementSpeed = .55f;
                pm.turnSmoothTime = .35f;

                cc.center = new Vector3(0, 0.21f, .16f);
                cc.radius = .11f;
                cc.height = .44f;
                
                break; 
            case 2:
                pm.movementSpeed = .5f;
                pm.turnSmoothTime = .35f;

                cc.center = new Vector3(0, 0.33f, .16f);
                cc.radius = .11f;
                cc.height = .68f;
                break;
        }
    }
    private void BoxAvailable()
    {
        if (boxDetected == false) return;

        distanceFromBox = transform.position.y - boxDetected.transform.position.y;
        if (distanceFromBox < .5f)
            boxAvailable = false;
        else
            boxAvailable = true;

    }
    private System.Collections.IEnumerator LetBox(float time)
    {
        //.22
        PlayerSingleton.Instance.grabingBox = false;
        PlayerSingleton.Instance.canMove = false;
        StartCoroutine(ResetMovement());
        yield return new WaitForSeconds(time);
        cc.center = Vector3.forward * .01f;
        cc.radius = .04f;
        cc.height = .3f;
        pm.turnSmoothTime = initialRotationSpeed;
        pm.movementSpeed = initialSpeed;
        pm.useGravity = true;

        boxGrabbed.transform.parent = null;
        boxGrabbed.GetComponent<Rigidbody>().useGravity = true;       
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        
        boxGrabbed.layer = 6;
        boxGrabbed = null;
        
    }
    private System.Collections.IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(1.9f);
        PlayerSingleton.Instance.canMove = true;
    }
    private void Falling()
    {
        if (PlayerSingleton.Instance.isGrounded == false&&PlayerSingleton.Instance.grabingBox)
        {
            StartCoroutine(LetBox(0));
            pm.useGravity = true;
        }
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, .1f))
        {
            if (hit.collider.CompareTag("Box")) boxDetected = hit.collider.gameObject;
            else boxDetected = null;
        }
        else boxDetected = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grabPos.position, .02f);
        Gizmos.DrawRay(transform.position, transform.forward * .1f);
    }

}