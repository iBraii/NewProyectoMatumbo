using UnityEngine;
using UnityEngine.UI;

public class SignInbtn : MonoBehaviour
{
    private Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(FindObjectOfType<GameJoltTrophies>().CompareTrophiesDelayed);
    }
}