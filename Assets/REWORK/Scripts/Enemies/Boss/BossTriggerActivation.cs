using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossTriggerActivation : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private AudioSource source;
    private int state=1;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 1)
            {
                transform.position = new Vector3(-2.4f, -9, -73.7f);
                state = 2;
                enemy.SetActive(true);
                source.Play();
                source.DOFade(.75f, 2);
            }
            if (state == 2)
            {
                source.DOFade(.15f, 5);
                GetComponent<BoxCollider>().enabled = false;
            }
           
        }
    }
    

}
