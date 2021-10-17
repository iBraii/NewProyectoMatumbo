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
        hints = new string[2];
        hints[0] = "You can hide in some places";
        hints[1] = "Use the dreamcatcher to protect yourself";

        hintsText.text = hints[Random.Range(0,2)];
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
