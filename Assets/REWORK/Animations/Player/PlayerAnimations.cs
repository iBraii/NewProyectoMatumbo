using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;
    PlayerSingleton ps;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        ps = PlayerSingleton.Instance;
    }

    private void Update()
    {
        anim.SetBool("isMoving", ps.isMoving);
        anim.SetBool("Hiding", ps.isHiding);
        anim.SetBool("Dying", ps.stress >= 10);
        anim.SetBool("Grabbing", ps.grabingBox);
        anim.SetBool("grounded", ps.isGrounded);
        anim.SetFloat("Yvelocity", GetComponent<PlayerMovement>().playerVelocity.y);
        anim.SetBool("isAttacking", ps.usingWeap);
    }
}
