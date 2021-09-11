using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEspectroIra : MonoBehaviour
{
    public string currentState;
    private float distanceToPlayer;
    private MeshRenderer activeMeshRenderer;
    public MeshRenderer fakeMeshRenderer;
    [HideInInspector]
    public bool isDenied;
    void Start()
    {
        activeMeshRenderer = GetComponent<MeshRenderer>();
        currentState = "Hiding";
    }
    private void Awake()
    {
        Dreams.onWeaponUsed += HandleDenied;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= HandleDenied;
    }

    // Update is called once per frame
    void Update()
    {
        if(!DenyEnemy.inRange)
            StateController();

    }
    private void StateController()
    {
        switch (currentState)
        {
            case "Hiding":
                HandleHiding();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject,1f))
                {
                    currentState = "Attack";                   
                }
                break;
            case "Attack":
                HandleAttack();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1.5f) == false)
                {
                    currentState = "Active"; 
                }
                break;
            case "Active":
                HandleActive();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 2.5f) == false)
                {
                    currentState = "Hiding";
                }else if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1f))
                {
                    currentState = "Attack";
                }
                break;
        }
    }

    private void HandleAttack()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;

        PlayerSingleton.Instance.stress += 1.5f * Time.deltaTime;
        Debug.Log("Current Stress: " + PlayerSingleton.Instance.stress);
    }
    private void HandleHiding()
    {
        activeMeshRenderer.enabled = false;
        fakeMeshRenderer.enabled = true;
    }
    private void HandleActive()
    {
        activeMeshRenderer.enabled = true;
        fakeMeshRenderer.enabled = false;
    }
    private void HandleDenied()
    {
        if (DenyEnemy.inRange)
        {
            activeMeshRenderer.enabled = true;
            fakeMeshRenderer.enabled = false;
        }
        
    }
}
