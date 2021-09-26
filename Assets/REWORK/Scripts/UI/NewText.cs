using UnityEngine;
using UnityEngine.UI;

public class NewText : MonoBehaviour
{
    public Text myTxt;
    public string texto;
    public int myCase;
    public CheatsCode cc;

    private void Update()
    {
        if(cc == null)
        {
            Debug.LogWarning("no se encuentra cheatcode");
            return;
        }
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
            
            //CHEATCODES
            case 5:
                myTxt.text = texto + cc.nc;
                break;
            case 6:
                myTxt.text = texto + cc.inmo;
                break;
            case 7:
                myTxt.text = texto + cc.infi;
                break;
            case 8:
                myTxt.text = texto + cc.spd;
                break;
        }
    }
}
