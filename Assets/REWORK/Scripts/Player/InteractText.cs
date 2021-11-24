using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InteractText : MonoBehaviour
{
    [SerializeField] private string interactableTag;
    public Text text;
    public int collectableIndex;
    public bool playerClose;

    private void OnTriggerEnter(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[collectableIndex]) return;
        if (other.CompareTag(interactableTag))
        {
            playerClose = true;
            text.DOFade(1, 1);
        }         
    }
    private void OnTriggerExit(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[collectableIndex]) return;
        if (other.CompareTag(interactableTag))
        {
            playerClose = false;
            text.DOFade(0, 1);
        }        
    }
}