using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DuckMission : MonoBehaviour
{
    public GameObject duckToSpawn;
    public GameObject sphereOfLight;
    private InteractText interact;
    public bool playerClose;
    public bool duckActive = false;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject llave;
    private float t;
    void Start()
    {
        source = GetComponent<AudioSource>();
        interact = FindObjectOfType<InteractText>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteraction();
        if (duckActive) AudioPlayer();
    }
    private void PlayerInteraction()
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (interact.playerClose == false) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (duckActive == false)
            {
                SoundManager.instance.Play("LlaveAgua");
                SoundManager.instance.Play("pichi");
                llave.transform.DORotate(new Vector3(-360, 0,0), 5, RotateMode.WorldAxisAdd);
                duckActive = true;
                duckToSpawn.SetActive(true);
                duckToSpawn.transform.parent = null;
                transform.position = duckToSpawn.transform.position;
            }
            else
            {
                SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
                SaveSystem.Save();
                if (FindObjectOfType<GameJoltTrophies>())
                    FindObjectOfType<GameJoltTrophies>().CompareTrophies();

                interact.text.DOFade(0, 1);
                interact.playerClose = false;
                sphereOfLight.transform.parent = null;
                sphereOfLight.SetActive(true);
                StartCoroutine(Disappear());
                AchievementPop.onMisionCompleted?.Invoke("You have found the duck");
            }
        }
    }
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(.5f);
        duckToSpawn.SetActive(false);
        gameObject.SetActive(false);
    }
    private void AudioPlayer()
    {
        t += Time.deltaTime;
        if (t >= 10)
        {
            t = 0;
            source.Play();
        }
    }
}
