using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCheats : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction resetAction;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        resetAction = playerInput.actions["ResetLevel"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
