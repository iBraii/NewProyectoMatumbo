using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private float jumpTimer;
    [SerializeField] private float maxJumpTimer;
    private bool jumpBool;
    //Referencias de clases
    private GrabBox gb;
    [SerializeField] private Dreams dc;
    private PlayerSingleton ps;
    private PlayerMovement pm;
    private NewHideInBlanket hib;
    [SerializeField] private StressManager sm;
    //Temporal
    public bool onAnim;
    public bool canMove;
    public bool usingWeap;
    public bool isMoving;
    public bool grounded;
    public bool grabingBox;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
        ps = PlayerSingleton.Instance;
        hib = GetComponentInParent<NewHideInBlanket>();
        //dc = GetComponentInParent<Dreams>();

        //Referencias de clases
        gb = GetComponentInParent<GrabBox>();
    }

    private void Update()
    {
        onAnim = PlayerSingleton.Instance.onAnimation;
        canMove = PlayerSingleton.Instance.canMove;
        usingWeap = PlayerSingleton.Instance.usingWeap;
        isMoving = PlayerSingleton.Instance.isMoving;
        grabingBox = PlayerSingleton.Instance.grabingBox;
        grounded = ps.isGrounded;
        if (pm.playerVelocity.y < 0)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= maxJumpTimer)
            {
                jumpBool = true;
                jumpTimer = 0;
            }
        }
        else if (ps.isGrounded) { jumpTimer = 0; jumpBool = false; }

        anim.SetBool("isMoving", ps.isMoving);

        //if(ps.isHiding)
        //    anim.SetBool("Hiding", true);
        //if(hib.unhide)
        //    anim.SetBool("Hiding", false);
        anim.SetBool("Hiding", ps.isHiding);

        anim.SetBool("Dying", ps.stress >= 10);
        anim.SetBool("Grabbing", ps.grabingBox);
        anim.SetBool("grounded", ps.isGrounded);
        anim.SetBool("Falling", jumpBool);
        anim.SetBool("isAttacking", dc.atrapAnim);
        anim.SetBool("IsOnDefeat", sm.isOnDefeat);
        anim.SetFloat("MovSpeed", pm.speed);
    }

    public void AnimationStart()
    {
        PlayerSingleton.Instance.onAnimation = true;
    }
    public void AnimationEnd()
    {
        PlayerSingleton.Instance.onAnimation = false;
    }
    public void Custom(string i)
    {
        switch (i)
        {
            case "Levantar":
                gb.MoveBox();
                break;
            case "ResetMovement":
                gb.ResetMovement();
                break;
            case "SoltarCaja":
                gb.LetBoxEvent();
                break;
            case "DCStateOn":
                dc.DCStateOn();
                break;
            case "DCStateOff":
                dc.DCStateOff();
                break;
            case "StaticJump":
                pm.Jump();
                break;
            case "DCOnHand":
                dc.DCOnHand();
                break;
            case "DCOnBack":
                if (dc.enabled)
                    dc.DCOnBack();
                else return;
                break;
            case "Step":
                pm.StepSFX(false);
                break;
            case "StepBox":
                pm.StepSFX(true);
                break;

        }
    }
}
