using UnityEngine;
using UnityEngine.UI;

public class TriggerTexts : MonoBehaviour
{
    [SerializeField] private string[] interactableTags;
    [SerializeField] private GameObject textObj;
    
    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < interactableTags.Length; i++)
        {
            if (other.gameObject.tag == interactableTags[i])
            {
                textObj.SetActive(true);

                switch (interactableTags[i])
                {
                    case "Cuadro":
                        textObj.GetComponent<Text>().text = "Press E to pick up family photo";
                        break;
                    case "Osito":
                        textObj.GetComponent<Text>().text = "Press E to pick up TeddyBear";
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < interactableTags.Length; i++)
        {
            if (other.gameObject.tag == interactableTags[i]) textObj.SetActive(false);
        }
    }
}
