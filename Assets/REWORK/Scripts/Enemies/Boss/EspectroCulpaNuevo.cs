using UnityEngine;

public class EspectroCulpaNuevo : MonoBehaviour
{
    [Header("Vars")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject player;
    [SerializeField] private bool active = true;

    public float currentSpeed;
    private int currentIndex;
    private float rotationTime;

    [Space]
    [SerializeField] private Transform[] waypoint;

    [Header("Dificultad Dinamica")]
    [SerializeField] private float minTreshhold;
    [SerializeField] private float maxTreshhold;

    private Animator anim;
    private AudioSource source;
    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        currentIndex = 0;
        currentSpeed = normalSpeed;
    }
    private void Update()
    {
        if (active == false) return;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        rotationTime += Time.deltaTime*rotationSpeed;
        Quaternion lookRotation = Quaternion.LookRotation(waypoint[currentIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationTime);

        transform.position = Vector3.MoveTowards(transform.position,waypoint[currentIndex].position , currentSpeed*Time.deltaTime);    

        if (transform.position == waypoint[currentIndex].position)
        {
            rotationTime = 0;
            CheckIfCompleted();       
        }
    }
    private void CheckIfCompleted()
    {
        if (currentIndex == waypoint.Length - 1) Deactivate();
        else
        {
            SpeedController();
            currentIndex++;
        }
    }
    private void SpeedController()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer < minTreshhold)
        {
            currentSpeed = slowSpeed;
            //anim.speed = 1.3f;
        }            
        else if (distanceToPlayer > minTreshhold && distanceToPlayer < maxTreshhold)
        {
            currentSpeed = normalSpeed;
            //anim.speed = 2.6f;
        }           
        else if (distanceToPlayer > maxTreshhold)
        {
            currentSpeed = fastSpeed;
            //anim.speed = 4;
        }
            
    }
    public void PlayStepSound()
    {
        source.pitch = Random.Range(.7f, 1);
        source.Play();
    }
    private void Deactivate()
    {
        active = false;
        anim.enabled = false;
    }
    private void OnDrawGizmos()
    {
        foreach(Transform waypoint in waypoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoint.position, .3f);
        }

        for(int i = 0; i < waypoint.Length - 1; i++) 
            Debug.DrawLine(waypoint[i].position, waypoint[i + 1].position, Color.red);   
    }
}
