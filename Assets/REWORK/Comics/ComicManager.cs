using UnityEngine;

public class ComicManager : MonoBehaviour
{
    [SerializeField] float maxComicTime;
    ChangeScene cs;
    string sceneName;
    [SerializeField] float appearBtnTime;
    [SerializeField] GameObject btn;

    private void Start()
    {
        cs = GameObject.Find("Transition").GetComponent<ChangeScene>();
        if (cs == null)
        {
            Debug.LogWarning("No se encontró Transition");
            return;
        }
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
            cs.Change(sceneName);
    }
    void AppearSkipButton()
    {
        appearBtnTime -= Time.deltaTime;
        if (appearBtnTime <= 0)
            btn.SetActive(true);
    }
    public void SkipBtn()
    {
        cs.Change(sceneName);
    }

    public GameObject[] comics;
    public static int comicNumber;
    void SetComic(int comicNumber)
    {
        switch (comicNumber)
        {
            case 1:
                comics[0].SetActive(true);
                sceneName = "NivelTutorial";
                break;
            case 2:
                comics[1].SetActive(true);
                sceneName = "Nivel1";
                break;
            case 3:
                comics[2].SetActive(true);
                sceneName = "Nivel2";
                break;
            case 4:
                comics[3].SetActive(true);
                sceneName = "Nivel3";
                break;
            case 5:
                comics[4].SetActive(true);
                sceneName = "Nivel4";
                break;
            case 6:
                comics[5].SetActive(true);
                sceneName = "Nivel5";
                break;
        }
    }
}
