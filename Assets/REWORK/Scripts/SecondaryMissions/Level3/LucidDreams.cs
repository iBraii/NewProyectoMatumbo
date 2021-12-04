using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class LucidDreams : MonoBehaviour
{
    private InteractText interact;
    private PlayerInput playerInput;
    /*[HideInInspector]*/ public bool hasDisc;
    private Transform[] discPos;
    [SerializeField] private Transform posParent;
    [SerializeField] private Transform reproductorPos;

   

    private void Awake()
    {
        interact = FindObjectOfType<InteractText>();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void Start()
    {
        discPos = new Transform[posParent.childCount];
        for (int i = 0; i < discPos.Length; i++)
            discPos[i] = posParent.GetChild(i);
        transform.position = discPos[Random.Range(0, discPos.Length)].position;
    }
    private void Update()
    {
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex]) return;
        if (interact.playerClose == false) return;
        if(playerInput.actions["Interact"].triggered && hasDisc == false)
        {
            SoundManager.instance.Play("Confirmation");
            hasDisc = true;
            GetComponent<MeshRenderer>().enabled = false;
            transform.position = reproductorPos.position;
            GetComponent<CapsuleCollider>().radius = 3;
        }
        else if(playerInput.actions["Interact"].triggered && hasDisc == true)
        {
            interact.text.DOFade(0, 1);
            interact.playerClose = false;
            SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
            SaveSystem.Save();
            if (FindObjectOfType<GameJoltTrophies>())
                FindObjectOfType<GameJoltTrophies>().CompareTrophies();
            AchievementPop.onMisionCompleted?.Invoke("Reproducing \"Lucid Dreams\"");
            reproductorPos.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}