using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FamilyPhoto : MonoBehaviour
{
    private InteractText interact;
    private PlayerInput playerInput;

    private void Awake()
    {
        interact = FindObjectOfType<InteractText>();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void Update()
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (interact.playerClose == false) return;
        if (playerInput.actions["Interact"].triggered)
        {
            interact.text.DOFade(0, 1);
            interact.playerClose = false;
            transform.GetChild(0).gameObject.SetActive(false);
            SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
            AchievementPop.onMisionCompleted?.Invoke("FAMILY PHOTO ACQUIRED");
        }        
    }
}