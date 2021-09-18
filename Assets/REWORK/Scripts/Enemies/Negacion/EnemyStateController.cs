using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class EnemyStateController : MonoBehaviour
{
    DenyEnemy de;
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
    [SerializeField] float damage;
    [SerializeField] float pathSpeed;
    [SerializeField] float followSpeed;
    [SerializeField] bool detectObstacle;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        de = GetComponent<DenyEnemy>();
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
        //RANGOS DE VISION
        onVisionRange = DetectPlayer.detection.CheckIfVisionRange(this.gameObject,visionAngle, visionRange);
        isClose = DetectPlayer.detection.CheckIfLessDistance(this.gameObject, closeRange);

        //OBSTACLE RAYCAST
        RaycastHit hit;
        Vector3 detection = DetectPlayer.detection.player.transform.position - transform.position;
        if(Physics.Raycast(transform.position, detection,out hit))
        {
            if (hit.collider.gameObject.tag=="Wall")
                detectObstacle=true;
            else
                detectObstacle = false;
        }
        Debug.DrawRay(transform.position, detection,Color.red);

        StateController();
        //Debug.Log(currentState);
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
        agent.speed = pathSpeed;
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
        if(detectObstacle == false)
        {
            if ((isClose || onVisionRange) && !PlayerSingleton.Instance.isHiding)
                currentState = EnemyStates.Following;
        }
    }
    void HandleFollow()
    {
        agent.speed = followSpeed;
        agent.SetDestination(DetectPlayer.detection.player.transform.position);

        //ROTATION
        Vector3 lookPos = DetectPlayer.detection.player.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        //DA�O 
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 0.4f))
        {
            PlayerSingleton.Instance.stress += damage * Time.deltaTime;
            PlayerSingleton.Instance.beingAttacked = true;
        }
        else
            PlayerSingleton.Instance.beingAttacked = false;

        //CHANGE CONDITIONS
        if(onVisionRange)
        {
            if (isClose == false && onVisionRange == false || PlayerSingleton.Instance.isHiding)
                currentState = EnemyStates.Confused;
        }
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
            currentState = EnemyStates.OnPath;
        }     
    }
    void CallDeny()
    {
        if (de.inRange)
            currentState = EnemyStates.Denied;     
    }
    void HandleDenied()
    {
        agent.isStopped = true;
        PlayerSingleton.Instance.beingAttacked = false;
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


    private void OnDrawGizmos()
    {
        foreach(Transform wp in waypoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(wp.transform.position, 0.3f);
        }
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Debug.DrawLine(waypoints[i].position, waypoints[i + 1].position, Color.green);
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
