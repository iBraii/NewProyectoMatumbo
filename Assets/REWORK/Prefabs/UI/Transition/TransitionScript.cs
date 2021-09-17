using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private ChangeScene cs;
    public string sceneName;

    private void Start()
    {
        cs = GetComponent<ChangeScene>();
        if (cs == null)
        {
            Debug.LogWarning("No hay changeScene script");
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            cs.Change(sceneName);  
    }
}
