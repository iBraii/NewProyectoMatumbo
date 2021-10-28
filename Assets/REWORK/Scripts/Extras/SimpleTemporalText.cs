using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class SimpleTemporalText : MonoBehaviour
{
    private GameObject player;
    private GameObject parent;
    void Start()
    {
        player = GameObject.Find("NewPlayer");
        parent = transform.parent.gameObject;
        parent.transform.DOLookAt(player.transform.position, 2, AxisConstraint.Y, Vector3.up);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(GetComponent<TextMeshPro>().DOFade(1, 1.5f));
        sequence.AppendInterval(2);
        sequence.Append(GetComponent<TextMeshPro>().DOFade(0, 1.5f));

        if (sequence.IsComplete()) Deactivate();

    }


    private void Deactivate() => gameObject.SetActive(false);
    
}
