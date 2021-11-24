using UnityEngine;
using DG.Tweening;

public class Reproductor : MonoBehaviour
{
    private InteractText interact;
    private LucidDreams disc;

    private void Awake()
    {
        interact = FindObjectOfType<InteractText>();
        disc = FindObjectOfType<LucidDreams>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (other.CompareTag("Player") && disc.hasDisc == false)
        {
            interact.text.text = "A CD is missing";
            interact.text.DOFade(1, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (other.CompareTag("Player") && disc.hasDisc == false)
        {
            interact.text.DOFade(0, 1);
            StartCoroutine(changeText());
        }
    }

    private System.Collections.IEnumerator changeText()
    {
        yield return new WaitUntil(() => interact.text.color.a == 0);
        interact.text.text = "\"E\" ? ? ?";
    }
}