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
        anim.SetBool("isHiding", ps.isHiding);
        anim.SetBool("Dying", ps.stress >= 10);
        anim.SetBool("Grabbing", GetComponent<GrabBox>().grabingBox);
    }

}
