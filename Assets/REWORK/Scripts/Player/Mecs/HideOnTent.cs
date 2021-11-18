using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class HideOnTent : MonoBehaviour
{
    public bool isHiding;
    [SerializeField] private GameObject generalVolume;
    private Vignette vig;
    private void Update()
    {
        PlayerSingleton.Instance.isHiding = isHiding;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpa"))
        {
            isHiding = true;
            Volume volume = generalVolume.GetComponent<Volume>();
            if(volume.profile.TryGet<Vignette>(out vig))
            {
                vig.smoothness.value=1f;
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carpa"))
        {
            isHiding = false;
            Volume volume = generalVolume.GetComponent<Volume>();
            if (volume.profile.TryGet<Vignette>(out vig))
            {
                vig.smoothness.value = .7f;
            }
        }
            
    }
}
