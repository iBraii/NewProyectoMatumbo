using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScene : MonoBehaviour
{
    ChangeScene cs;
    public Text hintsText;
    public string[] hints;
    void Start()
    {
        cs = GameObject.Find("TransitionScreen").GetComponent<ChangeScene>();
        hints = new string[3];
        hints[0] = "You can hide in some places";
        hints[1] = "Use the dreamcatcher to protect yourself";
        hints[2] = "Protect from covid";

        hintsText.text = hints[Random.Range(0,3)];
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadPrevLevel()
    {
        cs.Change(PlayerPrefs.GetString("prevLevel"));
    }
}
