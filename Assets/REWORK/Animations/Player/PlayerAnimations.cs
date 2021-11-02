using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerSingleton ps;
    private PlayerMovement pm;
    private float jumpTimer;
    [SerializeField] private float maxJumpTimer;
    private bool jumpBool;
    private NewHideInBlanket hib;
    private Dreams dc;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
        ps = PlayerSingleton.Instance;
        hib = GetComponent<NewHideInBlanket>();
        dc = GetComponent<Dreams>();
    }

    private void Update()
    {
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

        if(ps.isHiding)
            anim.SetBool("Hiding", true);
        if(hib.unhide)
            anim.SetBool("Hiding", false);

        anim.SetBool("Dying", ps.stress >= 10);
        anim.SetBool("Grabbing", ps.grabingBox);
        anim.SetBool("grounded", ps.isGrounded);
        anim.SetBool("Falling", jumpBool);
        anim.SetBool("isAttacking", dc.atrapAnim);
    }
}
