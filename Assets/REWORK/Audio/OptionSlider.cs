using UnityEngine;

public class OptionSlider : MonoBehaviour
{
    private UnityEngine.UI.Slider slider;

    private void Awake() => slider = GetComponent<UnityEngine.UI.Slider>();

    private void Start() => slider.onValueChanged.AddListener(FindObjectOfType<SoundManager>().SetVolume);
}