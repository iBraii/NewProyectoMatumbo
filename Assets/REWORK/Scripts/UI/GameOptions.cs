using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider sens;
    [SerializeField] private Slider volume;

    private void Awake()
    {
        if (sens == null || volume == null) { Debug.LogWarning("Algun slider no está asignado"); return; } 
    }
    void Start()
    {
        //ASIGNAR VALOR
        sens.value = Data.Instance.setting.sensitivity;
        volume.value = Data.Instance.setting.volume;

        SoundManager.instance.SetVolume(volume.value);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ApplySettings()
    {
        Data.Instance.setting.sensitivity = sens.value;
        Data.Instance.setting.volume = volume.value;

        Data.Instance.SaveSettings();
    }

    public void QuitGame() => Application.Quit();
}
