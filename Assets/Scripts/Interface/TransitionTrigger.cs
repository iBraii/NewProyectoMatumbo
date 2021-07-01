using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
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
        sc_scCh.Change(levelName);
        PlayerPrefs.SetInt(levelCompleted, 1);
    }
    public void TimeControl()
    {
        maxTimer += Time.deltaTime;
        //if (maxTimer <= 0)
        //{
        //    sc_scCh.Change(defeatName);
        //}
    }
}
