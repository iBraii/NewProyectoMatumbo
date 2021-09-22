using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Dreams : MonoBehaviour
{
    //Inputs
    PlayerInput playerInput;
    InputAction attackAction;

    PlayerSingleton ps;
    float timer;

    private void Awake()
    {
        ps = PlayerSingleton.Instance;
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }
    private void Update()
    {
        UseWeapon();
        ps.dreamEnergy = Mathf.Clamp(ps.dreamEnergy, 0, ps.maxDreamEnergy);
    }

    public static event Action onWeaponUsed;

    void UseWeapon()
    {
        if (ps.dreamEnergy > 0 && attackAction.ReadValue<float>() == 1)
        {
            ps.dreamEnergy -= 1 * Time.deltaTime;
            ps.weapUsedTime += Time.deltaTime;
            ps.usingWeap = true;
            ps.canMove = false;
            timer = 0;
            onWeaponUsed?.Invoke();
        }
        else
        {
            ps.canMove = true;
            ps.usingWeap = false;
            RegenWeapEnergy();
        }
    }

    void RegenWeapEnergy()
    {
        timer += Time.deltaTime;
        if(timer >= 3)
        {
            if (ps.dreamEnergy <= ps.maxDreamEnergy)
                ps.dreamEnergy += .15f * Time.deltaTime;
        }  
    }
}
