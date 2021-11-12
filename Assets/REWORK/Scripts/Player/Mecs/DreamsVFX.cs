using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class DreamsVFX : MonoBehaviour
{
    private VisualEffect dcFX;

    public AudioSource attackSFX;
    public float t;
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
        t = Mathf.Clamp(t, 0, 1);
        if (PlayerSingleton.Instance.usingWeap == false)
            dcFX.Stop();

        attackSFX.volume = Mathf.Lerp(0, 1, t);
        if (PlayerSingleton.Instance.usingWeap&&t<1)
            t += Time.deltaTime * 2f;
        else if(!PlayerSingleton.Instance.usingWeap && t > 0)
            t -= Time.deltaTime;
    }
}
