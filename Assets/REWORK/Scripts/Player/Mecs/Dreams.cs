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
    [HideInInspector] public bool atrapAnim;
    [SerializeField] private float saveDelay;

    void UseWeapon()
    {
        if (ps.dreamEnergy > 0 && attackAction.ReadValue<float>() == 1 && 
            PlayerSingleton.Instance.canUseDreamCatcher && !PlayerSingleton.Instance.isHiding &&
            PlayerSingleton.Instance.isGrounded)
        {
            saveDelay = 0;
            weapDelay += Time.deltaTime;
            atrapAnim = true;
            ps.canMove = false;
            if (weapDelay >= .54f)
            {
                ps.usingWeap = true;
                dreamCatcherUse = true;
                ps.dreamEnergy -= Time.deltaTime;
                ps.weapUsedTime += Time.deltaTime;
                timer = 0;
                onWeaponUsed?.Invoke();
            }
        }
        else if(ps.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
        {
            saveDelay += Time.deltaTime;
            atrapAnim = false;
            weapDelay = 0;
            if(saveDelay >= 1.1f)
            {
                saveDelay = 0;
                ps.canMove = true;
                ps.usingWeap = false;
            }
            dreamCatcherUse = false;
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
