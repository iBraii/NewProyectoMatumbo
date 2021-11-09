using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public ChangeScene cs;
    public string sceneName;
    public int comicNumber;
    public bool selectScreenColorToWhite;

    private void OnTriggerEnter(Collider other)
    {
        //CheckPoints.ResetStatics();

        if (cs == null)
        {
            Debug.LogWarning("No hay changescene script");
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().movementSpeed = 0;
            PlayerSingleton.Instance.stress = 0;
            if (selectScreenColorToWhite)
                cs.white = true;
            else
                cs.white = false;

            cs.SelectComic(comicNumber);
            cs.Change(sceneName);
        } 
    }
}
