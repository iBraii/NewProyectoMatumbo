using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class ImprovedUIManager : MonoBehaviour
{
    //ATRAPASUEÑOS
    [Header("UI atrapasueños")]
    [SerializeField] private CanvasGroup DC_canvasGroup;
    [SerializeField][Tooltip("Max time on the UI")] private float maxUIDC;
    [SerializeField] [Tooltip("Image filled")] private Image DCBar;
    private float timer;   

    public bool gameIsPaused = false;
    private PlayerInput playerInput;
    private InputAction escapeAction;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;

    [Header("Camara Freelook")]
    [SerializeField] private CinemachineFreeLook cm;

    [Header("Player")]
    [SerializeField] private StressManager playersm;

    public static ImprovedUIManager Instance;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        escapeAction = playerInput.actions["Escape"];
        Instance = this;
    }
    private void Start()
    {
        SetCameraSensitivity();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SoundManager.instance.Play("BG1");

        #region nulls
        if (pausePanel == null)
        {
            Debug.LogWarning("No hay pausepanel");
            return;
        }
        if (optionPanel == null)
        {
            Debug.LogWarning("No hay optionpanel");
            return;
        }
        #endregion
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
    }
    private void Update()
    {
        InputCheck();
        AtrapasueñosUI();
    }
    private void InputCheck()
    {
        if (escapeAction.triggered && playersm.isOnDefeat == false)
            PauseGame();
    }
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        CursorToggle();
    }
   
    public void CursorToggle()
    {
        #region nulls
        if (pausePanel == null)
        {
            Debug.LogWarning("No hay pausepanel");
            return;
        }
        if (optionPanel == null)
        {
            Debug.LogWarning("No hay optionpanel");
            return;
        }
        #endregion
        if (gameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausePanel.SetActive(false);
            optionPanel.SetActive(false);
        }
    }

    public void SetCameraSensitivity()
    {
        cm.m_XAxis.m_MaxSpeed= SaveSystem.data.sensitivity;
        cm.m_YAxis.m_MaxSpeed = SaveSystem.data.sensitivity / 250;
    }
    
    void AtrapasueñosUI()
    {
        #region nulls
        if (DCBar == null)
        {
            Debug.LogWarning("No hay DCBar");
            return;
        }
        #endregion

        DCBar.fillAmount = PlayerSingleton.Instance.dreamEnergy / PlayerSingleton.Instance.maxDreamEnergy;

        if (PlayerSingleton.Instance.usingWeap == false && PlayerSingleton.Instance.dreamEnergy >= PlayerSingleton.Instance.maxDreamEnergy && DC_canvasGroup.alpha==1)
        {
            timer += Time.deltaTime;
            if (timer >= maxUIDC)
            {
                DC_canvasGroup.DOFade(0, 1).SetEase(Ease.InOutSine);
                timer = 0;
            }
        }
        else if(PlayerSingleton.Instance.usingWeap&&DC_canvasGroup.alpha==0)
        {
            DC_canvasGroup.DOFade(1, 1);
        } 
    }   
}
