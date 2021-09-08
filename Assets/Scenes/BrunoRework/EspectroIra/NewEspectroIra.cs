using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEspectroIra : MonoBehaviour
{
    public string currentState;
    public float distanceToPlayer;
    private GameObject player;
    private MeshRenderer activeMeshRenderer;
    public MeshRenderer fakeMeshRenderer;
    void Start()
    {
        player = GameObject.Find("NewPlayer");
        activeMeshRenderer = GetComponent<MeshRenderer>();
        currentState = "Hiding";
    }

    // Update is called once per frame
    void Update()
    {
        StateController();
    }
    private void FixedUpdate()
    {
        CheckPlayerDistance();
    }
    private void StateController()
    {
        switch (currentState)
        {
            case "Hiding":
                HandleHiding();
                if (distanceToPlayer <= 1)
                {
                    currentState = "Attack";                   
                }
                break;
            case "Attack":
                HandleAttack();
                if (distanceToPlayer >= 1.5f)
                {
                    currentState = "Active"; 
                }
                break;
            case "Active":
                HandleActive();
                if (distanceToPlayer >= 2.5f)
                {
                    currentState = "Hiding";
                }else if (distanceToPlayer <= 1)
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
    private void CheckPlayerDistance()
    {
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }
        else
        {
            Debug.LogWarning("No hay ningun player GameObject!!!");
        }
        
    }
}
