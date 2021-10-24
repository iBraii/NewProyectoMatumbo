using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class TriggerTexts : MonoBehaviour
{
    [SerializeField] private Text textObj;

    //Interact 
    private PlayerInput playerInput;
    private InputAction interactAction;
    private bool onRange;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    private void Update()
    {
        if (SaveSystem.data.achievementCompleted[0]) return;
        Interact();
    }
    void Interact()
    {
        if(interactAction.triggered && onRange)
        {
            SoundManager.instance.Play("Confirmation2");
            AchievementPop.onMisionCompleted("FAMILY PHOTO ACQUIRED");
            textObj.DOFade(0, 1);
            SaveSystem.data.achievementCompleted[0] = true;
            SaveSystem.Save();
            this.enabled = false;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[0]) return;
        if (other.CompareTag("Cuadro"))
        {
            onRange = true;
            textObj.DOFade(1, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[0]) return;
        if (other.CompareTag("Cuadro"))
        {
            onRange = false;
            textObj.DOFade(0, 1);
        }
    }
}
