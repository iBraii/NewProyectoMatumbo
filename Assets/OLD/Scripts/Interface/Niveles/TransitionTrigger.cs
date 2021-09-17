using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTrigg : MonoBehaviour
{
    private ChangeScene sc_scCh;
    public string levelCompleted;
    public string levelName, defeatName;
    public float maxTimer;
    // Start is called before the first frame update
    void Start()
    {
        sc_scCh = GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeControl();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name == "Level2")
        {
        }
        PlayerPrefs.SetInt(levelCompleted, 1);
        sc_scCh.Change(levelName);    
    }
    public void TimeControl()
    {
        maxTimer += Time.deltaTime;
    }
}
