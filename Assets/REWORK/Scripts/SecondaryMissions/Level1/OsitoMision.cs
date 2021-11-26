using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class OsitoMision : MonoBehaviour
{
    //INTERACT TEXT
    private InteractText interact;

    public Transform[] childPosition;
    public Vector3[] position;
    public int currentIndex;
    public int maxIndex;

    private GameObject player;

    public bool misionCompleted;
    public GameObject emisionSphere;
    private AudioSource humming;

    public GameObject teddyNumber;

    void Start()
    {
        emisionSphere.SetActive(false);
        player = GameObject.Find("Player");
        interact = player.GetComponent<InteractText>();
        position = new Vector3[childPosition.Length];
        for (int i = 0; i < childPosition.Length; i++)
        {
            position[i] = childPosition[i].position;
        }
        maxIndex = position.Length-1;
        humming = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.position = position[currentIndex];
        emisionSphere.transform.position = transform.position;
        PlayerInteraction();
    }
    private void PlayerInteraction()
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (interact.playerClose == false) return;

        if (player.GetComponent<PlayerInput>().actions["Interact"].triggered)
        {
            if (currentIndex < maxIndex)
            {
                GameObject obj = Instantiate(teddyNumber);
                obj.transform.position = transform.position + Vector3.up * .1f;
                obj.GetComponentInChildren<TextMeshPro>().text = "" + (maxIndex+ - currentIndex);
                currentIndex++;               
                SoundManager.instance.Play("Confirmation");     
            }
            else
            {
                SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
                SaveSystem.Save();
                if (FindObjectOfType<GameJoltTrophies>())
                    FindObjectOfType<GameJoltTrophies>().CompareTrophies();
                interact.text.DOFade(0, 1);
                interact.playerClose = false;
                AchievementPop.onMisionCompleted?.Invoke("YOU HAVE COLLECTED THE TEDDY BEAR");
                misionCompleted = true;
                emisionSphere.SetActive(true);
                humming.DOFade(0, 2);
                Invoke("DeactivateTeddy", 2);
                
            }
            
        }
    }
    public void DeactivateTeddy() => gameObject.SetActive(false);
}
