using UnityEngine;

public class ComicManager : MonoBehaviour
{
    [SerializeField] float maxComicTime;
    public ChangeScene cs;
    string sceneName;
    [SerializeField] float appearBtnTime;
    [SerializeField] GameObject btn;

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
        maxComicTime -= Time.deltaTime;
        if(maxComicTime <= 0)
        {
            if (cs == null)
            {
                Debug.LogWarning("No se encontró Transition");
                return;
            }
            cs.Change(sceneName);
        }
    }
    void AppearSkipButton()
    {
        appearBtnTime -= Time.deltaTime;
        if (appearBtnTime <= 0)
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

    public GameObject[] comics;
    public static int comicNumber;
    void SetComic(int comicNumber)
    {
        switch (comicNumber)
        {
            case 1:
                sceneName = "NivelTutorial";
                comics[0].SetActive(true);
                break;
            case 2:
                sceneName = "Nivel1";
                comics[1].SetActive(true);
                break;
            case 3:
                sceneName = "Nivel2";
                comics[2].SetActive(true);
                break;
            case 4:
                sceneName = "Nivel3";
                comics[3].SetActive(true);
                break;
            case 5:
                sceneName = "Nivel4";
                comics[4].SetActive(true);
                break;
            case 6:
                sceneName = "Nivel5";
                comics[5].SetActive(true);
                break;
        }
    }
}
