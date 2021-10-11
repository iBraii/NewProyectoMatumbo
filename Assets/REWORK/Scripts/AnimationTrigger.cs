using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator anim;
    public string animationString;

    private void OnTriggerEnter(Collider other)
    {
        if(anim == null || animationString == null)
        {
            Debug.LogWarning("No hay animController o animString");
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
