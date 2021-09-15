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
    [Header("Variables deteccion")]
    public float detectRange;
    public float attackRange;
    public float activeRange;
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
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, detectRange) == false)
            currentState = Enemy2States.Active;
    }
    private void HandleHiding()
    {
        activeMeshRenderer.enabled = false;
        fakeMeshRenderer.enabled = true;

        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, attackRange))
            currentState = Enemy2States.Attack;
    }
    private void HandleActive()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;

        //CHANGE CONDITIONS 
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, activeRange) == false)
            currentState = Enemy2States.Hiding;
        else if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, attackRange))
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

        //CHANGE CONDITIONS
        if (PlayerSingleton.Instance.usingWeap == false)
            currentState = Enemy2States.Active;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(transform.position, detectRange);
        
        Gizmos.color = Color.blue;   
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;   
        Gizmos.DrawWireSphere(transform.position, activeRange);
    }
}
public enum Enemy2States
{
    Hiding,
    Active,
    Attack,
    Denied
}
