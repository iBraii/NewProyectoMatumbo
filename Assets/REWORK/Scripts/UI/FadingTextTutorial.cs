using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTextTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] textMesh;
    [SerializeField] private bool playerOnArea;

    private void Start()
    {
        foreach (GameObject go in textMesh)
            go.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject go in textMesh)
                go.GetComponent<Animator>().Play("FadeIn");
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
                go.GetComponent<Animator>().Play("FadeOut");
        }
    }
}
