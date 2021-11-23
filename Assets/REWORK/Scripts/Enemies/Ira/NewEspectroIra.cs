using UnityEngine;
using Utilities;

public class NewEspectroIra : MonoBehaviour
{
    private DenyEnemy de;
    private Enemy2States currentState;
    private Animator anim;
    private AudioSource source;

    [Header("Variables deteccion")]
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float activeRange;

    private float deniedTime;

    [SerializeField] private SphereCollider atkCollider;

    void Start() => currentState = Enemy2States.Hiding;

    private void Awake()
    {
        de = GetComponent<DenyEnemy>();
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        Dreams.onWeaponUsed += CallDeny;
    }
    private void OnDisable() => Dreams.onWeaponUsed -= CallDeny;
    void Update()
    {
        StateController();
        if (deniedTime < 0) deniedTime = 0;
    }
    private void StateController()
    {
        switch (currentState)
        {
            case Enemy2States.Hiding:
                source.Stop();
                anim.SetBool("Attacking", false);
                anim.SetBool("Stunned", false);
                HandleHiding();
                break;
            case Enemy2States.Attack:
                if (source.isPlaying == false) source.Play();
                HandleAttack();
                anim.SetBool("Attacking", true);
                anim.SetBool("Stunned", false);
                break;
            case Enemy2States.Active:
                source.Stop();
                anim.SetBool("Attacking", false);
                anim.SetBool("Stunned", false);
                HandleActive();
                break;
            case Enemy2States.Denied:
                anim.SetBool("Stunned", true);
                HandleDenied();
                break;
        }
    }

    private void HandleAttack()
    {
        if (source.isPlaying == false) source.Play();

        atkCollider.enabled = true;
       
        PlayerSingleton.Instance.stress += 1.5f * Time.deltaTime;

        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, detectRange) == false)
            currentState = Enemy2States.Active;

        //Rotation
        //Vector3 lookPos = DetectPlayer.detection.player.transform.position;
        //lookPos.y = transform.position.y;
        //transform.LookAt(lookPos);
    }
    private void HandleHiding()
    {
        StartCoroutine(WaitToCollider());
        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, attackRange))
            currentState = Enemy2States.Attack;
    }
    private void HandleActive()
    {
        StartCoroutine(WaitToCollider());
        //CHANGE CONDITIONS 
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, activeRange) == false && OurTimer.TimerCount(1.3f))
            currentState = Enemy2States.Hiding; 
        else if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, attackRange))
            currentState = Enemy2States.Attack;
    }
    private void CallDeny()
    {
        if(de.inRange)
            currentState = Enemy2States.Denied;
    }
    private void HandleDenied()
    {
        StartCoroutine(WaitToCollider());

        if (PlayerSingleton.Instance.usingWeap == true)
            deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= 0 && PlayerSingleton.Instance.usingWeap == false)
            deniedTime -= Time.deltaTime / 2.3f;

        if (deniedTime <= 0)
            currentState = Enemy2States.Active;
    }
    private System.Collections.IEnumerator WaitToCollider()
    {
        yield return new WaitUntil(() => PlayerSingleton.Instance.beingAttacked == false);
        atkCollider.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(transform.position, detectRange);
        
        Gizmos.color = Color.blue;   
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;   
        Gizmos.DrawWireSphere(transform.position, activeRange);
    }
}
public enum Enemy2States
{
    Hiding,
    Active,
    Attack,
    Denied
}
