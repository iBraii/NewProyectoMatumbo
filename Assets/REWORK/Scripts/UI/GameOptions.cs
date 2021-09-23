using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    public Slider sens;
    public Slider volume;
    string sensString, volumeString;

    private void Awake()
    {
        sensString = "Sensitivity";
        volumeString = "Volume";
        if (sens == null || volume == null)
        {
            Debug.LogWarning("No hay sliders asignados");
            return;
        }

        //ASIGNAR VALOR DEFAULT SI ES 0
        if (PlayerPrefs.GetFloat(sensString) == 0)       
            PlayerPrefs.SetFloat(sensString, sens.value);
        if(PlayerPrefs.GetFloat(volumeString)==0)
            PlayerPrefs.SetFloat(volumeString, volume.value);

    }
    void Start()
    {
        //OBTENER VALORES GRABADOS PARA LOS SLIDERS
        sens.value = PlayerPrefs.GetFloat(sensString);
        volume.value = PlayerPrefs.GetFloat(volumeString);

        //ASIGNAR VALORES DE VOLUMEN 
        SoundManager.instance.SetVolume(volume.value);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void ApplySettings()
    {
        //ASIGNAR VALORES DE LOS SLIDER
        PlayerPrefs.SetFloat(sensString, sens.value);
        PlayerPrefs.SetFloat(volumeString, volume.value);

        //ASIGNAR VALORES DE VOLUMEN 
        SoundManager.instance.SetVolume(volume.value);
    }


    public void QuitGame()
    {
        LoadScene.CloseGame();
    }
}
