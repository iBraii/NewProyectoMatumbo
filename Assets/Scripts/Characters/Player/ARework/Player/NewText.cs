using UnityEngine;
using UnityEngine.UI;

public class NewText : MonoBehaviour
{
    public Text myTxt;
    public string texto;
    private void Update()
    {
        myTxt.text = ""+PlayerSingleton.Instance.dreamEnergy;
    }
}
