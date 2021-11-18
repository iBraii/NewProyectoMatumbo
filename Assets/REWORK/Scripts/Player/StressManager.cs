using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StressManager : MonoBehaviour
{
    [Header("Transition screen script")]
    [SerializeField] private ChangeScene cs;
    public bool isOnDefeat;

    [Header("Stress values")]
    [SerializeField] [Tooltip("Valor de regeneracion")] private float regenValue;
    [SerializeField] [Tooltip("Tiempo antes de empezar a regenerar")] private float delay;
    private float stressCooldown;
    
    //StressFeedback
    [Header("Volume profile for stress feedback")]
    [SerializeField] private GameObject generalVolume;

    private Vignette vig;
    private ColorAdjustments cad;
    private ChromaticAberration cab;
    private FilmGrain fg;

    private bool deadSound;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject resetPanel;
    private CanvasGroup defeatCanvasGroup;
    [SerializeField] private CheckPoints checkPoints;
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
        SoundManager.instance.Play("TensionLoop");
        SoundManager.instance.Play("HeartBeat");
        SoundManager.instance.ChangeIndividualVolume("TensionLoop", 0.0001f);
        SoundManager.instance.ChangeIndividualVolume("HeartBeat", 0.0001f);

        defeatCanvasGroup = defeatPanel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        StressLimits();
        RegenStress(regenValue, delay);
        ChangeToDeafeatScene();
        SetStressFeedback();
        //Temporal
        //Debug.Log("StressActual: " + PlayerSingleton.Instance.stress);
        //Debug.Log("Atacado?: " + PlayerSingleton.Instance.beingAttacked);
    }
    float StressLimits() => PlayerSingleton.Instance.stress = Mathf.Clamp(PlayerSingleton.Instance.stress, 0f, 10f);

    private void SetStressFeedback()
    {
        SoundManager.instance.ChangeIndividualVolume("LowRumble", PlayerSingleton.Instance.stress / 10);
        SoundManager.instance.ChangeIndividualVolume("TensionLoop", PlayerSingleton.Instance.stress / 10);
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
        /*else
            Debug.Log("Asignar GeneralVolume post al stress manager");*/
        
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
        if (PlayerSingleton.Instance.stress >= 10)
        {
            PlayerSingleton.Instance.canMove = false;
            if (isOnDefeat == false)
            {
                isOnDefeat = true;
                ShowDefeat();
            }
        }
    }
    private void ShowDefeat()
    {
        defeatPanel.SetActive(true);
        defeatCanvasGroup.DOFade(1, 2).OnComplete(ResetScene);
    }
    private void ResetScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReloadScene()
    {
        StartCoroutine(checkPoints.LoadPositions());
        resetPanel.GetComponent<Image>().raycastTarget = true;      
        resetPanel.GetComponent<CanvasGroup>().DOFade(1, 2).OnComplete(HideEverything);
    }
    private void HideEverything()
    {
        PlayerSingleton.Instance.stress = 0;
        isOnDefeat = false;
        defeatCanvasGroup.alpha = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        resetPanel.GetComponent<Image>().raycastTarget = false;
        resetPanel.GetComponent<CanvasGroup>().DOFade(0, 2);
        defeatPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
            PlayerSingleton.Instance.beingAttacked = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Damage"))
            PlayerSingleton.Instance.beingAttacked = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Damage"))
            PlayerSingleton.Instance.beingAttacked = false;
    }
}