using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public ChangeScene cs;
    public string sceneName;
    public int comicNumber;
    public bool selectScreenColorToWhite;
    public int currentLevel;

    private void OnTriggerEnter(Collider other)
    {   
        if (cs == null)
        {
            Debug.LogWarning("No hay changescene script");
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            SaveSystem.data.levelCompleted[currentLevel] = true;

            if (FindObjectOfType<StressManager>().hasReceivedDamage == false)
                SaveSystem.data.levelCompletedNoDmg[currentLevel] = true;

            SaveSystem.Save();

            if (FindObjectOfType<GameJoltTrophies>())
                FindObjectOfType<GameJoltTrophies>().CompareTrophies();

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
