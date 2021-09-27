using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    public Material mat;
    public Button btn;
    public Color color1 , color2;

    private void Start()
    {
        mat.SetColor("_EmissionColor", color2);
    }
    private void Awake()
    {
        Button.onButtonChanged += ButtonColor;
    }
    private void OnDisable()
    {
        Button.onButtonChanged -= ButtonColor;
    }

    void ButtonColor()
    {
        if(mat == null || btn == null)
        {
            Debug.LogWarning("No hay mat o btn");
            return;
        }
        if(btn.buttonToggle)
            mat.SetColor("_EmissionColor", color1 );
        else
            mat.SetColor("_EmissionColor", color2 );
    }
}
