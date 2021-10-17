using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    //ANIM
    private Animator anim;

    //AUDIO
    [Header("Sonidos de enemigo")]
    [SerializeField] private AudioClip followingSound;
    [SerializeField] private AudioClip pathSound;

    //Denegación de enemigo
    private DenyEnemy de;

    //Movimiento
    private NavMeshAgent agent;
    private int wpIndex;
    [Space] [SerializeField] private Transform[] waypoints;

    //Estados
    private EnemyStates currentState;

    //Rangos de vision
    private bool onVisionRange, isClose;
    [Header("Rangos de vision")]
    [SerializeField] [Tooltip("Angulo de vision de cono")] [Range(10,180)] private float visionAngle;
    [SerializeField] [Tooltip("Rango de vision de cono")] private float visionRange;
    [SerializeField] [Tooltip("Rango de vision cercana")] private float closeRange;

    //Timers a cambiar
    private float confusedTimer, deniedTime;
    [Header("Confundido")]
    [SerializeField] [Tooltip("Maximo tiempo de confusion")] private float maxConfusedTime;

    [Header("Daño")]
    [SerializeField] private float damage;

    //Velocidades de estados
    [Header("Velocidades de estados")]
    [SerializeField] private float pathSpeed;
    [SerializeField] private float followSpeed;
    private float initialCloseRange;

    //Deteccion player
    private bool detectObstacle;
    [Header("Deteccion player")]
    [SerializeField] private LayerMask lm;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        de = GetComponent<DenyEnemy>();
        anim = GetComponentInChildren<Animator>();
        Dreams.onWeaponUsed += CallDeny;
    }
    private void OnDisable() => Dreams.onWeaponUsed -= CallDeny;
    void Start()
    {
        currentState = EnemyStates.OnPath;
        initialCloseRange = closeRange;
    }
    void ChangeSound(AudioClip newClip)
    {
        gameObject.GetComponent<AudioSource>().clip = newClip;
        if(gameObject.GetComponent<AudioSource>().isPlaying == false)
            gameObject.GetComponent<AudioSource>().Play();
    }
    private void Update()
    {
        //RANGOS DE VISION
        onVisionRange = DetectPlayer.detection.CheckIfVisionRange(this.gameObject,visionAngle, visionRange);
        isClose = DetectPlayer.detection.CheckIfLessDistance(this.gameObject, closeRange);

        //OBSTACLE RAYCAST
        Vector3 direction = DetectPlayer.detection.player.transform.position - transform.position;
        direction.y = 0;
        float distance = Vector3.Distance(DetectPlayer.detection.player.transform.position, transform.position);
        detectObstacle = Physics.Raycast(transform.position, direction, distance, lm);

        Debug.DrawRay(transform.position, direction , Color.red);

        StateController();
        AnimationController();
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

    void AnimationController()
    {
        anim.SetBool("Chasing", currentState == EnemyStates.Following);
        anim.SetBool("isDenied", currentState == EnemyStates.Denied);
        anim.SetBool("Confused", currentState == EnemyStates.Confused);
    }

    void HandlePath()
    {
        ChangeSound(pathSound);
        closeRange = initialCloseRange;
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
        if ((isClose || onVisionRange) && !PlayerSingleton.Instance.isHiding && !detectObstacle)
            currentState = EnemyStates.Following;
    }
    void HandleFollow()
    {
        ChangeSound(followingSound);
        closeRange = initialCloseRange * 2;
        agent.speed = followSpeed;
        agent.SetDestination(DetectPlayer.detection.player.transform.position);

        //ROTATION
        Vector3 lookPos = DetectPlayer.detection.player.transform.position - transform.position;
        lookPos.y = transform.position.y;  
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 2 * Time.deltaTime);

        //DAÑO 
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, 0.4f))
        {
            PlayerSingleton.Instance.stress += damage * Time.deltaTime;
            PlayerSingleton.Instance.beingAttacked = true;
        }
        else
            PlayerSingleton.Instance.beingAttacked = false;

        //CHANGE CONDITIONS
        if (isClose == false || PlayerSingleton.Instance.isHiding)
            currentState = EnemyStates.Confused;
    }
    void HandleConfused()
    {
        ChangeSound(pathSound);
        agent.isStopped = true;
        confusedTimer += Time.deltaTime;

        //CHANGE CONDITIONS
        if (confusedTimer >= maxConfusedTime || (PlayerSingleton.Instance.isHiding == false && (isClose && onVisionRange)))
        {
            agent.isStopped = false;
            confusedTimer = 0;
            currentState = EnemyStates.OnPath;
        }     
    }
    void CallDeny()
    {
        if (de.inRange) currentState = EnemyStates.Denied;     
    }
    void HandleDenied()
    {
        ChangeSound(pathSound);
        agent.isStopped = true;
        PlayerSingleton.Instance.beingAttacked = false;
        if (PlayerSingleton.Instance.usingWeap == false)
            deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= PlayerSingleton.Instance.weapUsedTime * 1.5f)
        {
            agent.isStopped = false;
            deniedTime = 0;
            PlayerSingleton.Instance.weapUsedTime = 0;
            if ((isClose || onVisionRange) && !detectObstacle && !PlayerSingleton.Instance.isHiding)
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
            Debug.DrawLine(waypoints[i].position, waypoints[i + 1].position, Color.green);
    }
}
public enum EnemyStates
{
    OnPath,
    Confused,
    Following,
    Denied
}
