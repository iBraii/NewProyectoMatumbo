using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] ChangeScene cs;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetString("position", "position1");
            cs.Change("Level2");
            
            Debug.Log("asdasd");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetString("position", "position2");
            cs.Change("Level2");
            
            Debug.Log("aaaaaaa");
        }
    }
    
}
