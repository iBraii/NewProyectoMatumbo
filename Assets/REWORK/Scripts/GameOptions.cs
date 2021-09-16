using UnityEngine;
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
    }
    void Start()
    {
        //OBTENER VALORES GRABADOS PARA LOS SLIDERS
        sens.value = PlayerPrefs.GetFloat(sensString);
        volume.value = PlayerPrefs.GetFloat(volumeString);

        //ASIGNAR VALORES DE VOLUMEN 
        SoundManager.instance.generalVolume = PlayerPrefs.GetFloat(volumeString);
        SoundManager.instance.SetVolume(volume.value);
    }

    public void ApplySettings()
    {
        //ASIGNAR VALORES DE LOS SLIDER
        PlayerPrefs.SetFloat(sensString, sens.value);
        PlayerPrefs.SetFloat(volumeString, volume.value);

        //ASIGNAR VALORES DE VOLUMEN 
        SoundManager.instance.generalVolume = PlayerPrefs.GetFloat(volumeString);
        SoundManager.instance.SetVolume(volume.value);
    }

    public void NoSavedSettings()
    {
        sens.value = PlayerPrefs.GetFloat(sensString);
        volume.value = PlayerPrefs.GetFloat(volumeString);
    }

    public void QuitGame()
    {
        LoadScene.CloseGame();
    }
}
