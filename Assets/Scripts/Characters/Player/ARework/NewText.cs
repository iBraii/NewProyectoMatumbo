using UnityEngine;
using UnityEngine.UI;

public class NewText : MonoBehaviour
{
    public Text myTxt;
    public string texto;
    private void Update()
    {
        myTxt.text ="Carga: "+ (GameObject.Find("NewPlayer").GetComponent<LightMec>().energy.ToString("00")+"-"+
            GameObject.Find("NewPlayer").GetComponent<PlayerMovement>().movementSpeed);
    }
}
