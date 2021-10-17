using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [Header("Animation to play")]
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(anim == null)
        {
            Debug.LogWarning("AnimationTrigger/ No hay animController");
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            anim.enabled = true;
            SoundManager.instance.Play("Door");
            Destroy(this.gameObject);
        }   
    }
}
