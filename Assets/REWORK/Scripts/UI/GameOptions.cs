using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider sens;
    [SerializeField] private Slider volume;
    [SerializeField] private GameObject blockPanel;

    private void Awake()
    {
        if (sens == null || volume == null) { Debug.LogWarning("GameOptions/ Algun slider no está asignado"); return; }
    }

    public void DeactivateOtherFunctions() => StartCoroutine(DeactivateBtns());

    private System.Collections.IEnumerator DeactivateBtns()
    {
        blockPanel.SetActive(true);

        yield return new WaitForSeconds(1.8f);

        blockPanel.SetActive(false);
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
