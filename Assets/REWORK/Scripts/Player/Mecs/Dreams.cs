using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Utilities;

public class Dreams : MonoBehaviour
{
    //Inputs
    PlayerInput playerInput;
    InputAction attackAction;

    PlayerSingleton ps;
    float timer;
    bool dreamCatcherUse;

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
    private float weapDelay;

    void UseWeapon()
    {
        if (ps.dreamEnergy > 0 && attackAction.ReadValue<float>() == 1 && 
            PlayerSingleton.Instance.canUseDreamCatcher && !PlayerSingleton.Instance.isHiding &&
            PlayerSingleton.Instance.isGrounded)
        {
            weapDelay += Time.deltaTime;
            if(weapDelay >= .5f)
            {
                dreamCatcherUse = true;
                ps.dreamEnergy -= Time.deltaTime;
                ps.weapUsedTime += Time.deltaTime;
                ps.usingWeap = true;
                ps.canMove = false;
                timer = 0;
                onWeaponUsed?.Invoke();
            }
        }
        else if(ps.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
        {
            weapDelay = 0;
            if(dreamCatcherUse) ps.canMove = true;
            dreamCatcherUse = false;
            ps.usingWeap = false;
            RegenWeapEnergy();
        }
    }

    void RegenWeapEnergy()
    {
        timer += Time.deltaTime;
        if(timer >= 3 && ps.dreamEnergy <= ps.maxDreamEnergy) 
            ps.dreamEnergy += .15f * Time.deltaTime;
    }
}
