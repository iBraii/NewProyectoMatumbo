using UnityEngine;
using UnityEngine.UI;

public class DefeatScene : MonoBehaviour
{
    private ChangeScene cs;
    [SerializeField] private Text hintsText;
    [SerializeField] private string[] hints;

    void Start()
    {
        cs = GameObject.Find("TransitionScreen").GetComponent<ChangeScene>();
        hintsText.text = hints[Random.Range(0, hints.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadPrevLevel()
    {
        if (cs == null) return;
        cs.Change(PlayerPrefs.GetString("prevLevel"));
    }
}
