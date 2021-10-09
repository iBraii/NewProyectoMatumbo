using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    //Input vars
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction jumpAction;

    //SmoothInputVars
    Vector2 currentInputVec;
    [SerializeField] float inputSmoothTime;
    Vector2 inputSmoothVel;

    //Movement vars
    public float movementSpeed;
    public float turnSmoothTime = .1f;
    float turnSmoothvelocity;
    Vector3 moveDirection;

    //jump vars
    float gravity = -4.5f;
    [SerializeField] float jumpForce;
    Vector3 playerVelocity;
    private AudioSource jumpSource;

    //WhenGrabbingBox
    public float boxDistance;
    public bool obstacleDetected;
    private Vector3 desiredMovement;
    public GameObject boxGrabbed;
    private RaycastHit hit;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        jumpAction = _playerInput.actions["Jump"];
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        PlayerSingleton.Instance.canMove = true;
        jumpSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerSingleton.Instance.canMove) MovementAndRotate();
        Gravity();
    }
    private void Update()
    {
        if (PlayerSingleton.Instance.canMove) Jumping();
        ObstacleDetection();
    }

    public void MovementAndRotate()
    {
        
        Vector2 input = _moveAction.ReadValue<Vector2>();
        currentInputVec = Vector2.SmoothDamp(currentInputVec, input, ref inputSmoothVel, inputSmoothTime);

        if (input != Vector2.zero)
        {
            //ROTATE========================================================

            float targetAngle = Mathf.Atan2(currentInputVec.x, currentInputVec.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);

            if (PlayerSingleton.Instance.canRotate && !PlayerSingleton.Instance.isHiding && obstacleDetected == false) transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //MOVE==========================================================

            if (!PlayerSingleton.Instance.isHiding && obstacleDetected == false)
            {
                PlayerSingleton.Instance.isMoving = true;
                _characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
        }
        else
        {
            PlayerSingleton.Instance.isMoving = false;
            moveDirection = Vector3.zero;
            currentInputVec = Vector2.zero;
        }
    }

    private void ObstacleDetection()
    {
        boxGrabbed = GetComponent<GrabBox>().boxGrabbed;

        if (PlayerSingleton.Instance.isMoving)
        {
            desiredMovement = moveDirection;
        }
        if (PlayerSingleton.Instance.grabingBox&&moveDirection==Vector3.zero)
        {
            obstacleDetected = Physics.Raycast(boxGrabbed.transform.position, desiredMovement, out hit, boxDistance) ||
                Physics.Raycast(boxGrabbed.transform.position - (boxGrabbed.transform.right * .120f), desiredMovement, out hit, boxDistance) ||
                Physics.Raycast(boxGrabbed.transform.position + (boxGrabbed.transform.right * .120f), desiredMovement, out hit, boxDistance);
        }
        else if(PlayerSingleton.Instance.grabingBox && moveDirection != Vector3.zero)
        {
            obstacleDetected = Physics.Raycast(boxGrabbed.transform.position, moveDirection, out hit, boxDistance) ||
                Physics.Raycast(boxGrabbed.transform.position - (boxGrabbed.transform.right * .120f), moveDirection, out hit, boxDistance) ||
                Physics.Raycast(boxGrabbed.transform.position + (boxGrabbed.transform.right * .120f), moveDirection, out hit, boxDistance);
        }

        if (PlayerSingleton.Instance.grabingBox == false)
            obstacleDetected = false;
    }

    private void Gravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);

        if (PlayerSingleton.Instance.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (playerVelocity.y > 0)
            playerVelocity += Vector3.up * gravity * Time.deltaTime;
    }
    private void Jumping()
    {
        if (jumpAction.triggered && PlayerSingleton.Instance.isGrounded&&!PlayerSingleton.Instance.isHiding&&PlayerSingleton.Instance.canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3f * gravity);
            if (jumpSource != null)
            {
                jumpSource.pitch = Random.Range(.95f, 1f);
                jumpSource.Play();
            }
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            Scene sc = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sc.name);
        }
    }
    private void OnDrawGizmos()
    {
        if (boxGrabbed!=null)
        {
            Debug.DrawRay(boxGrabbed.transform.position, desiredMovement * boxDistance, Color.red);
            Debug.DrawRay(boxGrabbed.transform.position - (boxGrabbed.transform.right * .120f), desiredMovement * boxDistance, Color.red);
            Debug.DrawRay(boxGrabbed.transform.position + (boxGrabbed.transform.right * .120f), desiredMovement * boxDistance, Color.red);
        }
        
    }
}
