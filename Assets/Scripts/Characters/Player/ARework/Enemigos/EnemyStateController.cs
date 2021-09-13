using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    EnemyStates currentState;
    NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;
    bool onVisionRange, isClose;
    [SerializeField] float visionRange;
    [SerializeField] float closeRange;
    [SerializeField] float visionAngle;
    int wpIndex;
    float confusedTimer, deniedTime;
    [SerializeField] float maxConfusedTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Dreams.onWeaponUsed += CallDeny;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= CallDeny;
    }
    void Start()
    {
        currentState = EnemyStates.OnPath;
    }
    private void Update()
    {
        onVisionRange = DetectPlayer.detection.CheckIfVisionRange(this.gameObject,visionAngle, visionRange);
        isClose = DetectPlayer.detection.CheckIfLessDistance(this.gameObject, closeRange);
        StateController();
    }

    void StateController()
    {
        switch (currentState)
        {
            case EnemyStates.OnPath:
                HandlePath();
                break;
            case EnemyStates.Following:
                HandleFollow();
                break;
            case EnemyStates.Confused:
                HandleConfused(); 
                break;
            case EnemyStates.Denied:
                HandleDenied();
                break;
        }
    }

    void HandlePath()
    {
        agent.SetDestination(waypoints[wpIndex].transform.position);

        float wpDistance = Vector3.Distance(transform.position, waypoints[wpIndex].transform.position);
        
        if (wpDistance <= agent.stoppingDistance)
        {
            wpIndex++;
            if (wpIndex >= waypoints.Length)
                wpIndex = 0;
            agent.SetDestination(waypoints[wpIndex].transform.position);
        }

        //CHANGE CONDITIONS
        if (isClose || onVisionRange)
            currentState = EnemyStates.Following;
    }
    void HandleFollow()
    {
        agent.SetDestination(DetectPlayer.detection.player.transform.position);

        //CHANGE CONDITIONS
        if (isClose == false && onVisionRange == false || PlayerSingleton.Instance.isHiding)
            currentState = EnemyStates.Confused;
    }
    void HandleConfused()
    {
        agent.isStopped = true;
        confusedTimer += Time.deltaTime;

        //CHANGE CONDITIONS
        if (confusedTimer >= maxConfusedTime)
        {
            agent.isStopped = false;
            confusedTimer = 0;
            if (isClose || onVisionRange && PlayerSingleton.Instance.isHiding == false)
                currentState = EnemyStates.Following;
            else
                currentState = EnemyStates.OnPath;
        }     
    }
    void CallDeny()
    {
        if (DenyEnemy.inRange)
            currentState = EnemyStates.Denied;     
    }
    void HandleDenied()
    {
        agent.isStopped = true;
        if (PlayerSingleton.Instance.usingWeap == false)
                deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= PlayerSingleton.Instance.weapUsedTime)
        {
            agent.isStopped = false;
            deniedTime = 0;
            PlayerSingleton.Instance.weapUsedTime = 0;
            if (isClose || onVisionRange)
                currentState = EnemyStates.Following;
            else
                currentState = EnemyStates.OnPath;
        }
    }
}
public enum EnemyStates
{
    OnPath,
    Confused,
    Following,
    Denied
}
