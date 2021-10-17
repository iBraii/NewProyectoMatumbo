using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TriggerTexts : MonoBehaviour
{
    [SerializeField] private string[] interactableTags;
    [SerializeField] private GameObject textObj;

    //Interact 
    private PlayerInput playerInput;
    private InputAction interactAction;
    private bool onRange;

    private void Awake()
    {
        playerInput = GameObject.Find("NewPlayer").GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    private void Update() => Interact();

    void Interact()
    {
        if(interactAction.triggered && onRange)
            Debug.Log("Call achievement action");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (textObj == null) return;
        for(int i = 0; i < interactableTags.Length; i++)
        {
            if (other.gameObject.tag == interactableTags[i])
            {
                textObj.SetActive(true);
                onRange = true;

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
        if (textObj == null) return;
        for (int i = 0; i < interactableTags.Length; i++)
        {
            if (other.gameObject.tag == interactableTags[i]) textObj.SetActive(false);
            onRange = false;
        }
    }
}
