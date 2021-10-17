using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Transition animation")]
    [SerializeField] private Animator transition;

    [Header("Time before changing scene")]
    [Tooltip("Recommended that this value is more or equal to the transition animation duration")]
    [SerializeField] private float duration;

    [Header("Select this transition default color at start of the scene")]
    public bool white;

    void Start()
    {
        if (white)
            gameObject.GetComponent<Image>().color = Color.white;
        else
            gameObject.GetComponent<Image>().color = Color.black;
    }
    public void Change(string sceneName) => StartCoroutine(TransitionLoadScene(sceneName));

    public void ChangeToWhite(bool SelectWhite) => white = SelectWhite;

    IEnumerator TransitionLoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(sceneName);
    }

    public void SelectComic(int comicNumber) => ComicManager.comicNumber = comicNumber;
    public void SetWhite()=> gameObject.GetComponent<Image>().color = Color.white;
    public void SetBlack()=> gameObject.GetComponent<Image>().color = Color.black;
}
