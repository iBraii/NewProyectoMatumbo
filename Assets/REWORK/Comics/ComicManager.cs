using UnityEngine;

public class ComicManager : MonoBehaviour
{
    [SerializeField] float maxComicTime;
    ChangeScene cs;
    [SerializeField] string sceneName;
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
}
