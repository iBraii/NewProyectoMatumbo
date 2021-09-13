using UnityEngine;
using UnityEngine.UI;

public class NewText : MonoBehaviour
{
    public Text myTxt;
    public string texto;
    public int myCase;
    private void Update()
    {
        switch(myCase)
        {
            case 1:
                myTxt.text = texto + PlayerSingleton.Instance.dreamEnergy.ToString("00");
                break;
            case 2:
                myTxt.text = texto + PlayerSingleton.Instance.lightEnergy.ToString("00");
                break;
            case 3:
                myTxt.text = texto + PlayerSingleton.Instance.isHiding;
                break;
            case 4:
                myTxt.text = texto + PlayerSingleton.Instance.stress.ToString("00");
                break;
        }
    }
}
