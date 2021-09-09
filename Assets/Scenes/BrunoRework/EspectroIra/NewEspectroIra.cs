using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEspectroIra : MonoBehaviour
{
    public string currentState;
    public float distanceToPlayer;
    private MeshRenderer activeMeshRenderer;
    public MeshRenderer fakeMeshRenderer;
    void Start()
    {
        activeMeshRenderer = GetComponent<MeshRenderer>();
        currentState = "Hiding";
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
            case "Hiding":
                HandleHiding();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject,1f))
                {
                    currentState = "Attack";                   
                }
                break;
            case "Attack":
                HandleAttack();
                if (DetectPlayer.detection.CheckIfMoreDistance(this.gameObject, 1.5f))
                {
                    currentState = "Active"; 
                }
                break;
            case "Active":
                HandleActive();
                if (DetectPlayer.detection.CheckIfMoreDistance(this.gameObject, 2.5f))
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
}
