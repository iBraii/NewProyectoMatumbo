using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class GrabBox : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction interactAction;
    public  GameObject boxDetected;
    public GameObject boxGrabbed;
    private PlayerMovement pm;
    private CharacterController cc;
    private float initialSpeed;
    private float initialRotationSpeed;

    [SerializeField] [Tooltip("Box new position")] private Transform grabPos;

    [SerializeField] private float distanceFromBox;
    private float initialAcceleration;
    private bool hasGrabbedBox=false;
    //private bool onAnim;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        pm = GetComponent<PlayerMovement>();
        initialSpeed = pm.movementSpeed;
        initialRotationSpeed = pm.turnSmoothTime;
        cc = GetComponent<CharacterController>();
        initialAcceleration = pm.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        DetectBox();
        BlockJumpingAndDC();
        MyInput();
        EmergencyLetBox();
        //Falling();
        
    }
    private void MyInput()
    { 
        if (interactAction.triggered && PlayerSingleton.Instance.isGrounded&&PlayerSingleton.Instance.isHiding==false)
        {
            if (boxGrabbed != null && PlayerSingleton.Instance.onAnimation == false)
            {
                PlayerSingleton.Instance.onAnimation = true;
                LetBox();
                cc.Move(-transform.forward * .03f);
            }
            else if (boxDetected != null&&!PlayerSingleton.Instance.onAnimation&&PlayerSingleton.Instance.grabingBox==false)
            {
                PlayerSingleton.Instance.onAnimation = true;
                Grab();
                cc.Move(-transform.forward * .05f);
            }
        }
    }
    public void Grab()
    {
        //onAnim = true;
        hasGrabbedBox = true;
        PlayerSingleton.Instance.grabingBox = true;
        PlayerSingleton.Instance.canMove = false;
        SoundManager.instance.Play("BoxLift");

        boxGrabbed = boxDetected;
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = true;
        //StartCoroutine(ResetMovement(1.5f));              
    }
    public void MoveBox()
    {
        Vector3 rotation = new Vector3(0, boxGrabbed.transform.localEulerAngles.y, 0);
        boxGrabbed.transform.parent = grabPos;
        boxGrabbed.transform.DOLocalMove(Vector3.zero, .4f, false);
        if (boxGrabbed.transform.localEulerAngles.x != Vector3.zero.x || boxGrabbed.transform.localEulerAngles.z != Vector3.zero.z)
            boxGrabbed.transform.DORotate(rotation, .27f, RotateMode.Fast);
        boxGrabbed.layer = 2;

        //pm.useGravity = false;
        //.27
        BoxIdentifier(boxGrabbed.GetComponent<BoxIdentifier>().boxType);
    }
    public void ResetMovement()
    {
        //yield return new WaitForSeconds(time);
        PlayerSingleton.Instance.canMove = true;
        //onAnim = false;
    }
    private void BoxIdentifier(int box)
    {
        switch (box)
        {
            case 0:
                PlayerSingleton.Instance.maxSpeed = .6f;
                pm.turnSmoothTime = .35f;
                pm.acceleration = initialAcceleration / 1.5f;
                cc.center = new Vector3(0, 0.047f, .1f);
                cc.radius = 0.170808f;
                cc.height = .4f;
                break;  
            case 1:
                PlayerSingleton.Instance.maxSpeed = .55f;
                pm.acceleration = initialAcceleration / 2f;
                pm.turnSmoothTime = .4f;

                cc.center = new Vector3(0, 0.15f, .1f);
                cc.radius = .2f;
                cc.height = .6f;
                
                break; 
            case 2:
                pm.acceleration = initialAcceleration / 2.5f;
                PlayerSingleton.Instance.maxSpeed = .5f;
                pm.turnSmoothTime = .5f;

                cc.center = new Vector3(0, 0.247f, .1f);
                cc.radius = .2f;
                cc.height = .8f;
                break;
        }
    }
    public void LetBox()
    {
        boxGrabbed.transform.parent = null;
        //.22
        //onAnim = true;
        PlayerSingleton.Instance.maxSpeed = .75f;
        PlayerSingleton.Instance.grabingBox = false;
        PlayerSingleton.Instance.canMove = false;
        pm.acceleration = initialAcceleration;
        //StartCoroutine(ResetMovement());
        cc.center = Vector3.forward * .01f;
        cc.radius = .04f;
        cc.height = .3f;
        pm.turnSmoothTime = initialRotationSpeed;
        pm.movementSpeed = initialSpeed;
        //pm.useGravity = true;
       
    }
    private void EmergencyLetBox()
    {
        if(PlayerSingleton.Instance.grabingBox==false && pm.acceleration != initialAcceleration&&hasGrabbedBox)
        {
            hasGrabbedBox = false;
            PlayerSingleton.Instance.maxSpeed = .75f;
            PlayerSingleton.Instance.canMove = true;
            boxGrabbed.GetComponent<Rigidbody>().useGravity = true;
            boxGrabbed.GetComponent<Rigidbody>().isKinematic = false;
            boxGrabbed.layer = 6;
            pm.acceleration = initialAcceleration;
            cc.center = Vector3.forward * .01f;
            cc.radius = .04f;
            cc.height = .3f;
            pm.turnSmoothTime = initialRotationSpeed;
            pm.movementSpeed = initialSpeed;
            Debug.Log("Emergency letbox");
        }

        if (PlayerSingleton.Instance.grabingBox == false && boxGrabbed != null&&PlayerSingleton.Instance.onAnimation==false)
        {
            boxGrabbed = null;
        }
    }
    public void LetBoxEvent()
    {
        //boxGrabbed.transform.parent = null;
        boxGrabbed.GetComponent<Rigidbody>().useGravity = true;
        boxGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        PlayerSingleton.Instance.canMove = true;
        boxGrabbed.layer = 6;
        boxGrabbed = null;
        hasGrabbedBox = false;
    }
    //public void ResetMovement()
    //{
    //    PlayerSingleton.Instance.canMove = true;
    //    //onAnim = false;
    //}
    private void Falling()
    {
        if (PlayerSingleton.Instance.isGrounded==false && PlayerSingleton.Instance.grabingBox)
        {
            if (pm.playerVelocity.y < -3)
            {
                LetBox();
            }
            //pm.useGravity = true;
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFromBox))
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
        Gizmos.DrawRay(transform.position, transform.forward * distanceFromBox);
    }

}