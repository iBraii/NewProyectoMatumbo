using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class StressManager : MonoBehaviour
{
    public ChangeScene cs;
    float stressCooldown;
    [SerializeField] float regenValue, delay;
    //StressFeedback
    public GameObject generalVolume;
    private Vignette vig;
    private ColorAdjustments cad;
    private ChromaticAberration cab;
    private FilmGrain fg;
    void Start()
    {
        #region nulls
        if (cs == null)
        {
            Debug.LogWarning("No hay cs Script");
            return;
        }
        #endregion

        SoundManager.instance.Play("LowRumble");
        SoundManager.instance.Play("HeartBeat");
    }

    void Update()
    {
        StressLimits();
        RegenStress(regenValue, delay);
        ChangeToDeafeatScene();
        SetStressFeedback();
    }
    float StressLimits() => PlayerSingleton.Instance.stress = Mathf.Clamp(PlayerSingleton.Instance.stress, 0f, 10f);

    private void SetStressFeedback()
    {
        SoundManager.instance.ChangeIndividualVolume("LowRumble", PlayerSingleton.Instance.stress / 10);
        SoundManager.instance.ChangeIndividualVolume("HeartBeat", PlayerSingleton.Instance.stress / 10);

        Volume volume = generalVolume.GetComponent<Volume>();
        if (volume.profile.TryGet<Vignette>(out vig)&& volume.profile.TryGet<ColorAdjustments>(out cad) && volume.profile.TryGet<ChromaticAberration>(out cab)
            && volume.profile.TryGet<FilmGrain>(out fg)&& volume != null)
        {
            vig.intensity.value = Mathf.Lerp(.35f, .65f, PlayerSingleton.Instance.stress / 10);
            cad.saturation.value = Mathf.Lerp(0, -100, PlayerSingleton.Instance.stress / 10);
            cab.intensity.value = Mathf.Lerp(0, 1, PlayerSingleton.Instance.stress / 10);
            fg.intensity.value = Mathf.Lerp(0, 1, PlayerSingleton.Instance.stress / 10);
        }
        else
            Debug.Log("Asignar GeneralVolume post al stress manager");
        
    }
    void RegenStress(float regenValue, float delay)
    {
        if (PlayerSingleton.Instance.stress > 0 && PlayerSingleton.Instance.beingAttacked == false)
        {
            stressCooldown += Time.deltaTime;
            if (stressCooldown >= delay)
            {
                PlayerSingleton.Instance.stress -= regenValue * Time.deltaTime;
            }
        }
        else if(PlayerSingleton.Instance.stress <= 0 || PlayerSingleton.Instance.beingAttacked)
            stressCooldown = 0;
    }
    private void ChangeToDeafeatScene()
    { 
        if(PlayerSingleton.Instance.stress >= 10)
        {
            PlayerSingleton.Instance.canMove = false;
            Scene scene = SceneManager.GetActiveScene();
            PlayerPrefs.SetString("prevLevel", scene.name);
            cs.Change("DefeatScene");
        }
    }
}