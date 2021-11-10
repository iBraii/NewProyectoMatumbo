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
    private float timer;

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
    [HideInInspector] public bool atrapAnim;
    private bool canMoveBool;

    void UseWeaponAnim()
    {
        if (ps.dreamEnergy > 0 && attackAction.ReadValue<float>() == 1 && 
            PlayerSingleton.Instance.canUseDreamCatcher && !PlayerSingleton.Instance.isHiding &&
            PlayerSingleton.Instance.isGrounded)
        {
            atrapAnim = true;
            canMoveBool = false;
            ps.canMove = false;
        }
        else if(ps.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
        {
            atrapAnim = false;
            RegenWeapEnergy();

            canMoveBool = true;
            ps.canMove = true;
            ps.usingWeap = false;
        }

        if (PlayerSingleton.Instance.dreamEnergy <= 0 || attackAction.ReadValue<float>() == 0)
        {
            PlayerSingleton.Instance.usingWeap = false;
            animationFinished = false;
        }
           
            
    }
    private void UseWeapon()
    {
        if (animationFinished == false) return;

        ps.usingWeap = true;
        ps.dreamEnergy -= Time.deltaTime;
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
