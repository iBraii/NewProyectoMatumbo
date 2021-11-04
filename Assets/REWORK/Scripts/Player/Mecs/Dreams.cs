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

    public GameObject atrapaMano;
    public GameObject atrapaCuerpo;

    //Usado para indicar cuando la animacion de sacar el atrapasueños ha terminado
    public bool animationFinished;
    private void Awake()
    {
        ps = PlayerSingleton.Instance;
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }
    private void Update()
    {
        UseWeaponAnim();
        UseWeapon();
        ps.dreamEnergy = Mathf.Clamp(ps.dreamEnergy, 0, ps.maxDreamEnergy);
    }

    public static event Action onWeaponUsed;
    private float weapDelay;
    [HideInInspector] public bool atrapAnim;
    private float saveDelay;
    private float otroDelay;
    private bool canMoveBool;

    void UseWeaponAnim()
    {
        if (ps.dreamEnergy > 0 && attackAction.ReadValue<float>() == 1 && 
            PlayerSingleton.Instance.canUseDreamCatcher && !PlayerSingleton.Instance.isHiding &&
            PlayerSingleton.Instance.isGrounded)
        {
            //otroDelay += Time.deltaTime;
            //if (otroDelay >= .1f)
            //{
            //    canMoveBool = false;
            //    saveDelay = 0;
            //    weapDelay += Time.deltaTime;
            //    atrapAnim = true;
            //    ps.canMove = false;
            //    if (weapDelay >= .54f)
            //    {
            //        ps.usingWeap = true;
            //        dreamCatcherUse = true;
            //        ps.dreamEnergy -= Time.deltaTime;
            //        ps.weapUsedTime += Time.deltaTime;
            //        timer = 0;
            //        onWeaponUsed?.Invoke();
            //    }
            //}    
            atrapAnim = true;
            canMoveBool = false;
            ps.canMove = false;
        }
        else if(ps.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
        {
            //otroDelay = 0;
            //saveDelay += Time.deltaTime;
            atrapAnim = false;
            //weapDelay = 0;
            //if (canMoveBool == false && saveDelay >= 1.1f)
            //{
            //    canMoveBool = true;
            //    ps.canMove = true;
            //    ps.usingWeap = false;
            //}
            dreamCatcherUse = false;
            RegenWeapEnergy();

            canMoveBool = true;
            ps.canMove = true;
            ps.usingWeap = false;
        }

        if (PlayerSingleton.Instance.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
            PlayerSingleton.Instance.usingWeap = false;
    }
    private void UseWeapon()
    {
        if (animationFinished == false) return;

        ps.usingWeap = true;
        dreamCatcherUse = true;
        ps.dreamEnergy -= Time.deltaTime;
        ps.weapUsedTime += Time.deltaTime;
        timer = 0;
        onWeaponUsed?.Invoke();
    }
    public void StopWeapon()
    {
        canMoveBool = true;
        ps.canMove = true;
        ps.usingWeap = false;
    }
    //Usado para alternar la variable animationFinished
    public void DCStateOn()
    {
        animationFinished = true;
    }
    public void DCStateOff()
    {
        animationFinished = false;
    }
    public void DCOnHand()
    {
        atrapaMano.SetActive(true);
        atrapaCuerpo.SetActive(false);
    }
    public void DCOnBack()
    {
        atrapaMano.SetActive(false);
        atrapaCuerpo.SetActive(true);
    }
    void RegenWeapEnergy()
    {
        timer += Time.deltaTime;
        if(timer >= 3 && ps.dreamEnergy <= ps.maxDreamEnergy) 
            ps.dreamEnergy += .15f * Time.deltaTime;
    }
}
