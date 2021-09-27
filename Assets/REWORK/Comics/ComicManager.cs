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
                break;
            case 2:
                sceneName = "Nivel1";
                break;
            case 3:
                sceneName = "Nivel2";
                break;
            case 4:
                sceneName = "Nivel3";
                break;
            case 5:
                sceneName = "Nivel4";
                break;
            case 6:
                sceneName = "Nivel5";
                break;
        }
    }
}
