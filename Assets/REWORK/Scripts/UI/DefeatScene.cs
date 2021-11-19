using UnityEngine;
using UnityEngine.UI;

public class DefeatScene : MonoBehaviour
{
    [SerializeField] private Text hintsText;
    [SerializeField] private string[] hints;

    public void AsignText() => hintsText.text = hints[Random.Range(0, hints.Length)];
}