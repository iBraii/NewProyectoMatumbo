using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator transition;
    public float duration;
    public static bool white;
    // Start is called before the first frame update
    void Start()
    {
        if (white)
            gameObject.GetComponent<Image>().color = Color.white;
        else
            gameObject.GetComponent<Image>().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Change(string sceneName)
    {
        if (white)
            gameObject.GetComponent<Image>().color = Color.white;
        else
            gameObject.GetComponent<Image>().color = Color.black;

        StartCoroutine(TransitionLoadScene(sceneName));
    }

    public void ChangeToWhite(bool SelectWhite)
    {
        white = SelectWhite;
    }

    IEnumerator TransitionLoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneName);
    }

    public void SelectComic(int comicNumber)
    {
        ComicManager.comicNumber = comicNumber;
    }
}
