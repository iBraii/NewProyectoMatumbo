using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FamilyPhoto : MonoBehaviour
{
    [SerializeField] private InteractText interact;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        //interact = FindObjectOfType<InteractText>();
        //playerInput = FindObjectOfType<PlayerInput>();
    }
    private void Update()
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (interact.playerClose == false) return;
        if (playerInput.actions["Interact"].triggered)
        {
            interact.text.DOFade(0, 1);
            gameObject.SetActive(false);      
            AchievementPop.onMisionCompleted?.Invoke("FAMILY PHOTO ACQUIRED");

            SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
            SaveSystem.Save();
            if (FindObjectOfType<GameJoltTrophies>())
                FindObjectOfType<GameJoltTrophies>().CompareTrophies();
            interact.playerClose = false;
        }        
    }
}