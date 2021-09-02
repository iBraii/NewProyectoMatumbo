using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction jumpAction;
    bool grounded;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        jumpAction.performed += Jump;
    }
    private void OnDisable()
    {
        jumpAction.performed -= Jump;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("floor"))
            grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
            grounded = false;
    }
    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("I jumped");
    }
}
