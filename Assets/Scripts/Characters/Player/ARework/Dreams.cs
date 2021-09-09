using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Dreams : MonoBehaviour
{
    //Inputs
    PlayerInput playerInput;
    InputAction attackAction;

    PlayerSingleton ps;

    private void Awake()
    {
        ps = PlayerSingleton.Instance;
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }

    private void Update()
    {
        UseWeapon();
        Debug.Log(ps.energy);
    }

    public static event Action onWeaponUsed;

    void UseWeapon()
    {
        if (ps.energy >= 0 && attackAction.ReadValue<float>() == 1)
        {
            ps.energy -= 1 * Time.deltaTime;
            ps.usingWeap = true;
            onWeaponUsed?.Invoke();
        }
        else
        {
            ps.usingWeap = false;
            StartCoroutine(RegenWeapEnergy());
        }
    }

    IEnumerator RegenWeapEnergy()
    {
        yield return new WaitForSeconds(1);

        if (ps.energy <= ps.maxEnergy)
        {
            ps.energy += 1 * Time.deltaTime;
        }
        
    }
}
