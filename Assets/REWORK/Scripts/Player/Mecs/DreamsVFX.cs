using UnityEngine;
using UnityEngine.VFX;

public class DreamsVFX : MonoBehaviour
{
    private VisualEffect dcFX;
    private void Awake() => Dreams.onWeaponUsed += UsingDreamVFX;
    private void OnDisable() => Dreams.onWeaponUsed -= UsingDreamVFX;
    private void Start()
    {
        dcFX = GetComponent<VisualEffect>();
        dcFX.enabled = true;
    }
    void UsingDreamVFX() => dcFX.Play();
    void Update()
    {
        if (PlayerSingleton.Instance.usingWeap == false)
            dcFX.Stop();
    }
}
