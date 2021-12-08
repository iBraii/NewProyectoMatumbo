using UnityEngine;
using UnityEngine.Video;

public class ComicManager : MonoBehaviour
{
    private VideoPlayer video;
    public ChangeScene cs;
    private string sceneName;
    public VideoClip[] comics;
    public static int comicNumber;

    [SerializeField] private float appearBtnTime;
    [SerializeField] private GameObject btn;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }
    private void Start()
    {
        SetComic(comicNumber);
    }
    private void Update()
    {
        ComicManagement();
        AppearSkipButton();
    }
    void ComicManagement()
    {
        if (video.frame > 0 && video.isPlaying == false)
        {
            FindObjectOfType<ChangeScene>().Change(sceneName);
            enabled = false;
        }
    }
    void AppearSkipButton()
    {
        appearBtnTime -= Time.deltaTime;
        if (appearBtnTime <= 0 || Input.anyKey)
            btn.SetActive(true);
    }
    public void SkipBtn()
    {
        if (cs == null)
        {
            Debug.LogWarning("No se encontró Transition");
            return;
        }
        cs.Change(sceneName);
    }


    void SetComic(int comicNumber)
    {
        switch (comicNumber)
        {
            case 0:
                sceneName = "NivelTuto";
                video.clip = comics[0];
                break;
            case 1:
                sceneName = "Nivel1";
                video.clip = comics[1];
                break;
            case 2:
                sceneName = "Nivel2";
                video.clip = comics[2];
                break;
            case 3:
                sceneName = "Nivel3";
                video.clip = comics[3];
                break;
            case 4:
                sceneName = "Nivel4";
                video.clip = comics[4];
                break;
            case 5:
                sceneName = "Nivel4.5";
                video.clip = comics[5];
                break;
        }
    }
}
