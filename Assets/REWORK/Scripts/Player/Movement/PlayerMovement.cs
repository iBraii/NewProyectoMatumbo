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
    [HideInInspector] public Vector3 playerVelocity;
    private AudioSource jumpSource;

    //WhenGrabbingBox

    [HideInInspector] public bool useGravity;

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
        initialTurnTime = turnSmoothTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerSingleton.Instance.canMove) MovementAndRotate();
        Gravity();
    }
    private void Update()
    {
        Jumping();  
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

            if (PlayerSingleton.Instance.canRotate && !PlayerSingleton.Instance.isHiding ) transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //MOVE==========================================================

            if (!PlayerSingleton.Instance.isHiding)
            {
                PlayerSingleton.Instance.isMoving = true;
                _characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
        }
        else PlayerSingleton.Instance.isMoving = false;

        if (PlayerSingleton.Instance.isGrounded && PlayerSingleton.Instance.grabingBox==false)
            turnSmoothTime = initialTurnTime;        
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
        if (PlayerSingleton.Instance.grabingBox || !PlayerSingleton.Instance.isGrounded || !PlayerSingleton.Instance.canMove || ImprovedUIManager.Instance.gameIsPaused||PlayerSingleton.Instance.isHiding)
            PlayerSingleton.Instance.canJump = false;
        else
            PlayerSingleton.Instance.canJump = true;

        if (jumpAction.triggered &&PlayerSingleton.Instance.canJump)
        {
            Jump();
            GetComponentInChildren<Animator>().SetBool("JumpTrigger",true);
        }
    }
    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpForce * 3f);
        if (jumpSource != null)
        {
            jumpSource.pitch = Random.Range(.95f, 1f);
            jumpSource.Play();
        }
        Invoke("ResetJump", 0.2f);
    }
    private void ResetJump() => GetComponentInChildren<Animator>().SetBool("JumpTrigger", false);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            Scene sc = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sc.name);
        }
    }
}