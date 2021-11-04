using UnityEngine;
using Utilities;

public class NewEspectroIra : MonoBehaviour
{
    private DenyEnemy de;
    private Enemy2States currentState;
    private Animator anim;
    private AudioSource source;

    [Space]
    [SerializeField] private GameObject activeMeshRenderer;
    [SerializeField] private GameObject fakeMeshRenderer;

    [Header("Variables deteccion")]
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float activeRange;

    private float deniedTime;

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
        AnimatorController();
    }
    private void StateController()
    {
        switch (currentState)
        {
            case Enemy2States.Hiding:
                HandleHiding();
                break;
            case Enemy2States.Attack:
                HandleAttack();
                break;
            case Enemy2States.Active:
                HandleActive();
                break;
            case Enemy2States.Denied:
                HandleDenied();
                break;
        }
    }
    void AnimatorController()
    {
        anim.SetBool("isScreaming", currentState == Enemy2States.Attack);
        anim.SetBool("isDenied", currentState == Enemy2States.Denied);
    }

    private void HandleAttack()
    {
        if (source.isPlaying == false) source.Play();

        PlayerSingleton.Instance.beingAttacked = true;
        activeMeshRenderer.SetActive(true);
        fakeMeshRenderer.SetActive(false);

        PlayerSingleton.Instance.stress += 1.5f * Time.deltaTime;

        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, detectRange) == false)
            currentState = Enemy2States.Active;
    }
    private void HandleHiding()
    {
        activeMeshRenderer.SetActive(false);
        fakeMeshRenderer.SetActive(true);
        PlayerSingleton.Instance.beingAttacked = false;
        //CHANGE CONDITIONS
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject, attackRange))
            currentState = Enemy2States.Attack;
    }
    private void HandleActive()
    {
        activeMeshRenderer.SetActive(true);
        fakeMeshRenderer.SetActive(false);
        PlayerSingleton.Instance.beingAttacked = false;
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
        activeMeshRenderer.SetActive(true);
        fakeMeshRenderer.SetActive(false);
        PlayerSingleton.Instance.beingAttacked = false;

        if (PlayerSingleton.Instance.usingWeap == false)
            deniedTime += Time.deltaTime;

        //CHANGE CONDITIONS
        if (deniedTime >= PlayerSingleton.Instance.weapUsedTime * 1.5f)
        {
            deniedTime = 0;
            PlayerSingleton.Instance.weapUsedTime = 0;
            currentState = Enemy2States.Active;
        }
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
