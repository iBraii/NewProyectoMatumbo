using UnityEngine;
using UnityEngine.SceneManagement;

public class StressManager : MonoBehaviour
{
    CanvasGroup stressPanel;
    public ChangeScene cs;
    float stressCooldown;
    [SerializeField] float regenValue, delay;

    void Start()
    {
        #region nulls
        if (cs == null)
        {
            Debug.LogWarning("No hay cs Script");
            return;
        }
        #endregion
        stressPanel = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        StressLimits();
        BlinkPanel();
        RegenStress(regenValue, delay);
        ChangeToDeafeatScene();
    }
    void BlinkPanel()
    {
        #region PanelAlpha
        if (PlayerSingleton.Instance.stress >= 7f)
            stressPanel.alpha = Mathf.PingPong(Time.time * .3f, .4f);
        else if (PlayerSingleton.Instance.stress >= 8f)
            stressPanel.alpha = Mathf.PingPong(Time.time * .3f, .6f);
        else if (PlayerSingleton.Instance.stress >= 9f)
            stressPanel.alpha = Mathf.PingPong(Time.time * .3f, .8f);
        #endregion
    }

    float StressLimits() => PlayerSingleton.Instance.stress = Mathf.Clamp(PlayerSingleton.Instance.stress, 0f, 10f);

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