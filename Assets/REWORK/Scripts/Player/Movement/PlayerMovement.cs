using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Controller
    private CharacterController _characterController;

    //Input vars
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction jumpAction;

    //SmoothInputVars
    private Vector2 currentInputVec;
    private Vector2 inputSmoothVel;
    [Header("Smoothed input")]
    [SerializeField] private float inputSmoothTime;

    //Movement vars
    [Header("Movement variables")]
    public float movementSpeed;
    public float turnSmoothTime = .1f;
    private float initialTurnTime;
    private float turnSmoothvelocity;
    private Vector3 moveDirection;

    //jump vars
    [SerializeField] private float gravity = -4.5f;
    [Header("Jump Force")] public float jumpForce;
    /*[HideInInspector]*/ public Vector3 playerVelocity;
    [SerializeField]private AudioSource jumpSource;
    [SerializeField]private AudioSource stepSource;
    private Animator anim;

    //WhenGrabbingBox

    [HideInInspector] public bool useGravity;
    public float speed;
    public float acceleration;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        jumpAction = _playerInput.actions["Jump"];
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        PlayerSingleton.Instance.canMove = true;
        initialTurnTime = turnSmoothTime;
        PlayerSingleton.Instance.maxSpeed = .75f;
    }

    void FixedUpdate()
    {
        if(PlayerSingleton.Instance.canMove&&!PlayerSingleton.Instance.onAnimation) MovementAndRotate();
        Gravity();
    }
    private void Update()
    {
        if(!PlayerSingleton.Instance.onAnimation) Jumping();

        Acceleration();
        anim.SetBool("JumpTrigger", jumpAction.triggered);
    }

    public Vector3 lastMove;

    public void MovementAndRotate()
    {
        
        Vector2 input = _moveAction.ReadValue<Vector2>();
        currentInputVec = Vector2.SmoothDamp(currentInputVec, input, ref inputSmoothVel, inputSmoothTime);

        float targetAngle = Mathf.Atan2(currentInputVec.x, currentInputVec.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);


        if (input != Vector2.zero)
        {
            //ROTATE========================================================

            if (PlayerSingleton.Instance.canRotate && !PlayerSingleton.Instance.isHiding) transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //MOVE==========================================================

            if (!PlayerSingleton.Instance.isHiding)
            {
                PlayerSingleton.Instance.isMoving = true;
                _characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
                lastMove = moveDirection;
            }
        }
        else
        {
            if (PlayerSingleton.Instance.canRotate && !PlayerSingleton.Instance.isHiding && movementSpeed > 0.5f) 
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if (!PlayerSingleton.Instance.isHiding)
                 _characterController.Move(lastMove * movementSpeed * Time.deltaTime);
            if(movementSpeed <= 0)
                PlayerSingleton.Instance.isMoving = false;
        }

        if (PlayerSingleton.Instance.isGrounded && PlayerSingleton.Instance.grabingBox==false)
            turnSmoothTime = initialTurnTime;
    }

    public void StepSFX(bool box)
    {
        if (!box)
        {
            float pitch = Random.Range(.65f, .75f);
            stepSource.pitch = pitch;
            stepSource.Play();
        }
        else
        {
            stepSource.pitch = .7f;
            stepSource.Play();
        }    
        
    }
    private void Acceleration()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        if (input!=Vector2.zero && speed < 1)
            speed += Time.deltaTime* acceleration;
        else if(input == Vector2.zero && speed > 0)
            speed -= Time.deltaTime * acceleration; // speed = 0;

        /*if (speed > 1)
            speed = 1;
        else if (speed < 0)
            speed = 0;*/

        speed = Mathf.Clamp(speed, 0, 1);

        movementSpeed = Mathf.Lerp(0, PlayerSingleton.Instance.maxSpeed, speed);

    }
    private void Gravity()
    {
        if (useGravity == false) return;
        playerVelocity.y += gravity*3f * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);

        if (PlayerSingleton.Instance.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (playerVelocity.y > 0)
            playerVelocity += Vector3.up * gravity * Time.deltaTime;
    }

    private void Jumping()
    {
        if (PlayerSingleton.Instance.isGrounded)
        {
            if (PlayerSingleton.Instance.grabingBox || !PlayerSingleton.Instance.canMove || ImprovedUIManager.Instance.gameIsPaused || PlayerSingleton.Instance.isHiding)
                PlayerSingleton.Instance.canJump = false;
            else
                PlayerSingleton.Instance.canJump = true;
        }
        else PlayerSingleton.Instance.canJump = false;

        if (jumpAction.triggered && PlayerSingleton.Instance.canJump && PlayerSingleton.Instance.isMoving)
            Invoke("Jump", .1f);
    }
    public void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpForce * 3f);
        if (jumpSource != null)
        {
            jumpSource.pitch = Random.Range(.95f, 1f);
            jumpSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathBox"))
        {
            PlayerSingleton.Instance.canMove = false;
            PlayerSingleton.Instance.stress = 10;
        }
    }
}