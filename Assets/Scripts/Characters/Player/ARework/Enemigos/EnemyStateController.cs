using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    EnemyStates currentState;
    NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(waypoints[0].position);
        Dreams.onWeaponUsed += HandleDenied;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= HandleDenied;
    }
    void Start()
    {
        currentState = EnemyStates.OnPath;
    }
    private void Update()
    {
        if (DenyEnemy.inRange == false)
            StartCoroutine(StateController());
    }

    IEnumerator StateController()
    {
        switch (currentState)
        {
            case EnemyStates.OnPath:
                HandlePath();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1f))
                    currentState = EnemyStates.Following;
                break;
            case EnemyStates.Following:
                HandleFollow();
                if (DetectPlayer.detection.CheckIfMoreDistance(this.gameObject, 1f))
                    currentState = EnemyStates.Confused;
                break;
            case EnemyStates.Confused:
                HandleConfused();
                if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 1f))
                    currentState = EnemyStates.Following;
                else
                {
                    yield return new WaitForSeconds(3);
                    if (DetectPlayer.detection.CheckIfMoreDistance(this.gameObject, 1f))
                        currentState = EnemyStates.OnPath;
                }  
                break;
        }
    }

    void HandlePath()
    {
        
    }
    void HandleFollow()
    {
        
    }
    void HandleConfused()
    {
        
    }
    void HandleDenied()
    {
        if (DenyEnemy.inRange)
            Debug.Log("DENIED");
    }
}
public enum EnemyStates
{
    OnPath,
    Confused,
    Following
}
