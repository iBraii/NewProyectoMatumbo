using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicationTrigger : MonoBehaviour
{
    public string customEvent;
    public GameObject obj_indicationController;
    public IndicationsTutorial sc_indicationsTutorial;
    void Start()
    {
        obj_indicationController = GameObject.Find("IdicationController");
        sc_indicationsTutorial = obj_indicationController.GetComponent<IndicationsTutorial>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (customEvent)
            {
                case "SpawnEnemy":
                    sc_indicationsTutorial.ActivateEnemy();
                    this.gameObject.SetActive(false);
                    break;
                case "DreamCatcherHint":
                    sc_indicationsTutorial.dreamCatcherHint= true;
                    this.gameObject.SetActive(false);
                    break;

                case "button":
                    sc_indicationsTutorial.learningButton = true;
                    this.gameObject.SetActive(false);
                    break;
                case "learnDark":
                    sc_indicationsTutorial.learningDark = true;
                    this.gameObject.SetActive(false);
                    break;
                case "DCP":
                    sc_indicationsTutorial.ActivateDCP();
                    this.gameObject.SetActive(false);
                    break;
                case "DarkArea":
                    sc_indicationsTutorial.ActivateDarkArea();
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
