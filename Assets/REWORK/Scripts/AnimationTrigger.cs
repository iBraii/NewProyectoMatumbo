using UnityEngine;
using DG.Tweening;
public class AnimationTrigger : MonoBehaviour
{
    [Header("Animation to play")]
    [SerializeField] private Animator anim;
    public bool other=false;
    public string soundToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if(anim == null)
        {
            Debug.LogWarning("AnimationTrigger/ No hay animController");
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (other == false) 
            {
                anim.enabled = true;
                SoundManager.instance.Play("Door");
                Destroy(this.gameObject);
            }
            else
            {
                Other();
            }
            
        }   
    }
    private void Other()
    {
        anim.enabled = true;
        SoundManager.instance.Play(soundToPlay);
        Destroy(this.gameObject);
    }
}
