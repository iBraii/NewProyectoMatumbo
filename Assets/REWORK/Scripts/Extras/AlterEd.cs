using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlterEd : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip dissapearClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            source.clip = dissapearClip;
            source.Play();
            source.DOFade(0, 6).OnComplete(Deactivate);
            foreach(Material mat in GetComponent<MeshRenderer>().materials)
            {
                mat.DOFade(0, 2);
            }
        }
    }
    private void Deactivate() => gameObject.SetActive(false);
}
