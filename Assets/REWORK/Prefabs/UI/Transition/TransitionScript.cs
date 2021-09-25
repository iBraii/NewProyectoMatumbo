using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public ChangeScene cs;
    public string sceneName;
    public int comicNumber;

    private void Start()
    {
        if (cs == null)
        {
            Debug.LogWarning("No hay changeScene script");
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cs.SelectComic(comicNumber);
            cs.Change(sceneName);
        } 
    }
}
