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
    public Vector2 currentInputVec;
    [SerializeField] float inputSmoothTime;
    Vector2 inputSmoothVel;

    //Movement vars
    public float movementSpeed;
    /*[HideInInspector]*/ public Vector3 moveDirection; //Direction of camera
    public float turnSmoothTime = .1f;
    float turnSmoothvelocity;

    //jump vars
    float gravity = -4.5f;
    [SerializeField] float jumpForce;
    Vector3 playerVelocity;
    private AudioSource jumpSource;


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
        if(PlayerSingleton.Instance.canMove) MoveAndRotate();
        Gravity();
    }
    private void Update() { if (PlayerSingleton.Instance.canMove) Jumping(); }

    public void MoveAndRotate()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        currentInputVec = Vector2.SmoothDamp(currentInputVec, input, ref inputSmoothVel, inputSmoothTime);
        Vector3 move = new Vector3(currentInputVec.x, 0f, currentInputVec.y);
        move = move.x * Camera.main.transform.right.normalized + move.z * Camera.main.transform.forward.normalized;
        move.y = 0;
        _characterController.Move(move * movementSpeed * Time.deltaTime);

        if (input != Vector2.zero && !PlayerSingleton.Instance.isHiding)
        {
            PlayerSingleton.Instance.isMoving = true;
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);

            if (PlayerSingleton.Instance.canRotate) transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else PlayerSingleton.Instance.isMoving = false;
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
            jumpSource.pitch = Random.Range(.95f, 1f);
            jumpSource.Play();
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
}
