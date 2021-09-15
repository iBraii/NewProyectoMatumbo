using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEspectroIra : MonoBehaviour
{
    DenyEnemy de;
    Enemy2States currentState;
    private MeshRenderer activeMeshRenderer;
    public MeshRenderer fakeMeshRenderer;
    [HideInInspector]
    public bool isDenied;
    float deniedTime;

    void Start()
    {
        de = GetComponent<DenyEnemy>();
        activeMeshRenderer = GetComponent<MeshRenderer>();
        currentState = Enemy2States.Hiding;
    }
    private void Awake()
    {
        Dreams.onWeaponUsed += CallDeny;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= CallDeny;
    }

    // Update is called once per frame
    void Update()
    {
        StateController();
    }
    private void StateController()
    {
        switch (currentState)
        {
            case Enemy2States.Hiding:
                HandleHiding();
                break;
            case Enemy2States.Attack:
                HandleAttack();
                break;
            case Enemy2States.Active:
                HandleActive();
                break;
            case Enemy2States.Denied:
                HandleDenied();
                break;
        }
    }

    private void HandleAttack()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;

        PlayerSingleton.Instance.stress += 1.5f * Time.deltaTime;
        Debug.Log("Current Stress: " + PlayerSingleton.Instance.stress);

        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1.5f) == false)
            currentState = Enemy2States.Active;
    }
    private void HandleHiding()
    {
        activeMeshRenderer.enabled = false;
        fakeMeshRenderer.enabled = true;

        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1f))
            currentState = Enemy2States.Attack;
    }
    private void HandleActive()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;

        //CHANGE CONDITIONS 
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 2.5f) == false)
            currentState = Enemy2States.Hiding;
        else if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1f))
            currentState = Enemy2States.Attack;
    }
    private void CallDeny()
    {
        if(de.inRange)
            currentState = Enemy2States.Denied;
    }
    private void HandleDenied()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;

        if (PlayerSingleton.Instance.usingWeap == false)
            deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= PlayerSingleton.Instance.weapUsedTime)
        {
            deniedTime = 0;
            PlayerSingleton.Instance.weapUsedTime = 0;
            currentState = Enemy2States.Active;
        }
    }
}
public enum Enemy2States
{
    Hiding,
    Active,
    Attack,
    Denied
}
