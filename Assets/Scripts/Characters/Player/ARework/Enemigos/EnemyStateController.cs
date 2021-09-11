using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    EnemyStates currentState;
    NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;
    [SerializeField] bool onVisionRange, isClose;
    [SerializeField] float visionRange;
    [SerializeField] float closeRange;
    [SerializeField] float visionAngle;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(waypoints[0].position);
        Dreams.onWeaponUsed += HandleDenied;;
        
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
        onVisionRange = DetectPlayer.detection.CheckIfVisionRange(this.gameObject,visionAngle, visionRange);
        isClose = DetectPlayer.detection.CheckIfLessDistance(this.gameObject, closeRange);
        if (DenyEnemy.inRange == false)
            StartCoroutine(StateController());
        Debug.Log(onVisionRange);
    }

    IEnumerator StateController()
    {
        switch (currentState)
        {
            case EnemyStates.OnPath:
                HandlePath();
                if (isClose || onVisionRange)
                    currentState = EnemyStates.Following;
                break;
            case EnemyStates.Following:
                HandleFollow();
                if (isClose == false && onVisionRange == false)
                    currentState = EnemyStates.Confused;
                break;
            case EnemyStates.Confused:
                HandleConfused();
                if (isClose || onVisionRange)
                    currentState = EnemyStates.Following;
                else
                {
                    yield return new WaitForSeconds(3);
                    if (isClose == false && onVisionRange == false)
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
