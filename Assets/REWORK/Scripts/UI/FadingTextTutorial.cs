using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FadingTextTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] textMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject go in textMesh)
                go.GetComponent<TextMeshPro>().DOFade(1, 2);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject go in textMesh)
                go.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject go in textMesh)
                go.GetComponent<TextMeshPro>().DOFade(0, 2);
        }
    }
}
