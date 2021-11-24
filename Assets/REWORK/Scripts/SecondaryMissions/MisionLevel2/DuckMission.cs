using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMission : MonoBehaviour
{
    public GameObject duckToSpawn;
    public GameObject sphereOfLight;
    public bool playerClose;
    public bool duckActive = false;
    [SerializeField] private AudioSource source;
    private float t;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteraction();
        if (duckActive)
            AudioPlayer();
    }
    private void PlayerInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerClose&&duckActive==false)
            {
                duckActive = true;
                duckToSpawn.SetActive(true);
                duckToSpawn.transform.parent = null;
                transform.position = duckToSpawn.transform.position;
            }else if((playerClose && duckActive == true))
            {
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
        }
    }
}
