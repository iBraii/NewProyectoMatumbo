using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    //ANIM
    private Animator anim;

    //AUDIO
    [Header("Sonidos de enemigo")]
    [SerializeField] private AudioClip[] sounds;

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
    [SerializeField]private Collider atkCollider;
    //Stress manager para que el enemigo no pueda detectar al jugador si esta en la pantalla de derrota;
    private StressManager sm;

    private Vector3 initialPos;

    private void OnDisable()
    {
        DetectPlayer.detection.player.GetComponent<StressManager>().onPlayerDead -= Restart;
        Dreams.onWeaponUsed -= CallDeny;
    }
    private void Awake()
    {
        DetectPlayer.detection.player.GetComponent<StressManager>().onPlayerDead += Restart;
        initialPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        de = GetComponent<DenyEnemy>();
        anim = GetComponentInChildren<Animator>();
        Dreams.onWeaponUsed += CallDeny;
    }
    void Start()
    {
        sm = DetectPlayer.detection.player.GetComponent<StressManager>();
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

        if (deniedTime < 0) deniedTime = 0;
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
        ChangeSound(sounds[0]);
        closeRange = initialCloseRange;
        agent.speed = pathSpeed;
        agent.SetDestination(waypoints[wpIndex].transform.position);
        StartCoroutine(WaitToCollider());
        float wpDistance = Vector3.Distance(transform.position, waypoints[wpIndex].transform.position);
        
        if (wpDistance <= agent.stoppingDistance)
        {
            wpIndex++;
            if (wpIndex >= waypoints.Length)
                wpIndex = 0;
            agent.SetDestination(waypoints[wpIndex].transform.position);
        }

        //CHANGE CONDITIONS
        if ((isClose || onVisionRange) && !PlayerSingleton.Instance.isHiding && !detectObstacle&&sm.isOnDefeat==false)
            currentState = EnemyStates.Following;
    }
    void HandleFollow()
    {
        ChangeSound(sounds[1]);
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
            atkCollider.enabled = true;
        }     

        //CHANGE CONDITIONS
        if (isClose == false || PlayerSingleton.Instance.isHiding)
            currentState = EnemyStates.Confused;
    }
    void HandleConfused()
    {
        ChangeSound(sounds[0]);
        agent.isStopped = true;
        confusedTimer += Time.deltaTime;
        StartCoroutine(WaitToCollider());
        //CHANGE CONDITIONS
        if (confusedTimer >= maxConfusedTime)
        {
            agent.isStopped = false;
            confusedTimer = 0;
            currentState = EnemyStates.OnPath;
        }
        else if(isClose && !PlayerSingleton.Instance.isHiding)
        {
            agent.isStopped = false;
            confusedTimer = 0;
            currentState = EnemyStates.Following;
        }
    }
    void CallDeny()
    {
        if (de.inRange&&detectObstacle==false) currentState = EnemyStates.Denied;     
    }
    void HandleDenied()
    {
        ChangeSound(sounds[2]);
        agent.isStopped = true;
        StartCoroutine(WaitToCollider());
        if (PlayerSingleton.Instance.usingWeap == true)
            deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= 0 && PlayerSingleton.Instance.usingWeap == false)
        {
            deniedTime -= Time.deltaTime / 2.3f;
            agent.isStopped = false;          
        }
        if(deniedTime <= 0)
        {
            if ((isClose || onVisionRange) && !detectObstacle && !PlayerSingleton.Instance.isHiding)
                currentState = EnemyStates.Following;
            else
                currentState = EnemyStates.OnPath;
        }    
    }
    private void Restart()
    {
        currentState = EnemyStates.OnPath;
        transform.position = initialPos;
        wpIndex =0;
    }
    private System.Collections.IEnumerator WaitToCollider()
    {
        yield return new WaitUntil(() => PlayerSingleton.Instance.beingAttacked == false);
        atkCollider.enabled = false;
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