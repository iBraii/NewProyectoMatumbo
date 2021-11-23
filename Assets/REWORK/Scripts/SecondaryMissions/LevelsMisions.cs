using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class LevelsMisions : MonoBehaviour
{
    public Transform[] childPosition;
    public Vector3[] position;
    public int currentIndex;
    public int maxIndex;

    public bool playerClose;
    private GameObject player;

    public bool misionCompleted;
    public GameObject emisionSphere;
    private AudioSource humming;

    public GameObject teddyNumber;
    [SerializeField] private Text textObj;
    [SerializeField] private int collectableIndex;

    void Start()
    {
        emisionSphere.SetActive(false);
        player = GameObject.Find("Player");
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
        if (playerClose == false) return;

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
                SaveSystem.data.achievementCompleted[collectableIndex] = true;
                SaveSystem.Save();
                AchievementPop.onMisionCompleted?.Invoke("YOU HAVE COLLECTED THE TEDDY BEAR");
                misionCompleted = true;
                emisionSphere.SetActive(true);
                humming.DOFade(0, 2);
                Invoke("DeactivateTeddy", 2);
                
            }
            
        }
    }
    public void DeactivateTeddy() => gameObject.SetActive(false);
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[collectableIndex]) return;
        if (other.CompareTag("Player"))
        {
            playerClose = true;
            textObj.DOFade(1, 1);
        }          
    }
    private void OnTriggerExit(Collider other)
    {
        if (SaveSystem.data.achievementCompleted[collectableIndex]) return;
        if (other.CompareTag("Player"))
        {
           playerClose = false;
           textObj.DOFade(0, 1);
        }
    }

}
