using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AchievementPop : MonoBehaviour
{
    public static Action<string> onMisionCompleted;
    private void OnEnable() => onMisionCompleted += ShowAchievement;
    private void OnDisable() => onMisionCompleted -= ShowAchievement;
    
    private void ShowAchievement(string text)
    {
        SoundManager.instance.Play("Confirmation2");
        GetComponentInChildren<Text>().text = text;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(GetComponent<CanvasGroup>().DOFade(1, 2));
        sequence.AppendInterval(3);
        sequence.Append(GetComponent<CanvasGroup>().DOFade(0, 1));
    }
}
