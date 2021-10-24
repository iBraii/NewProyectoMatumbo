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
        sens.value = SaveSystem.data.sensitivity;
        volume.value = SaveSystem.data.volume;
        SoundManager.instance.SetVolume(volume.value);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    [ContextMenu("Save")]
    public void ApplySettings()
    {
        SaveSystem.data.sensitivity = sens.value;
        SaveSystem.data.volume = volume.value;
        SaveSystem.Save();
    }
    public void QuitGame() => Application.Quit();
}
