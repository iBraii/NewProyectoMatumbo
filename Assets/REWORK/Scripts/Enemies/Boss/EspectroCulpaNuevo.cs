using UnityEngine;

public class EspectroCulpaNuevo : MonoBehaviour
{
    [Header("Vars")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float rotationSpeed;

    private float speed;
    private int currentIndex;
    private float rotationTime;

    [Space]
    [SerializeField] private Transform[] waypoint;

    [Header("Dificultad Dinamica")]
    [SerializeField] private float minTreshhold;
    [SerializeField] private float maxTreshhold; 

    private void Start()
    {
        currentIndex = 0;
        speed = normalSpeed;
    }
    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        rotationTime += Time.deltaTime*rotationSpeed;
        Quaternion lookRotation = Quaternion.LookRotation(waypoint[currentIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationTime);

        transform.position = Vector3.MoveTowards(transform.position,waypoint[currentIndex].position , speed*Time.deltaTime);    

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
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (distanceToPlayer < minTreshhold)
            speed = slowSpeed;
        else if (distanceToPlayer > minTreshhold && distanceToPlayer < maxTreshhold)
            speed = normalSpeed;
        else if (distanceToPlayer > maxTreshhold)
            speed = fastSpeed;
    }
    private void Deactivate() => currentIndex = 0;
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
