using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossTriggerActivation : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private MeshRenderer finalLight;
    private AudioSource source;
    public int state=1;
    public bool canTrigger=true;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 1&&canTrigger)
            {
                canTrigger = false;
                StartCoroutine(Delay());
                transform.position = new Vector3(-2.4f, -9, -72.53f);
                state = 2;
                enemy.SetActive(true);
                source.Play();
                source.DOFade(.75f, 2);
                Camera.main.GetComponent<Animator>().enabled=true;
                Debug.Log("Inicio");
            }
            if (state == 2 && canTrigger)
            {
                canTrigger = false;
                source.DOFade(0, 5);
                finalLight.material.DOFade(0, 2);
                GetComponent<BoxCollider>().enabled = false;
                SoundManager.instance.Stop("BG1");
                Debug.Log("Fin");
            }
           
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        canTrigger = true;
    }
    

}
