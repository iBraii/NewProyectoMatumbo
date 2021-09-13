using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mylight : MonoBehaviour
{
    public bool lightStatus;
    public bool onLight;
    public Text placeHolderText;
    public Text placeHolderText2;
    public Text placeHolderText3;
    [SerializeField]
    private float deathTimer;

    private float dcCharge;
    [SerializeField]
    private bool dead;
    void Start()
    {
        UpdateText();
        dead = false;
        dcCharge = 10;
    }

    
    void Update()
    {
        TurnLight();
        Enemy();
        DCLight();
        UpdateText();

        placeHolderText3.text =""+ deathTimer;
    }
    private void DCLight()
    {
        if (lightStatus&&dcCharge>0)
        {
            dcCharge -= Time.deltaTime;
        }

        if (onLight&&dcCharge<10)
            dcCharge += Time.deltaTime;

        if (dcCharge <= 0)
        {
            lightStatus = false;
            dcCharge = 0;
            UpdateText();
        }
    }
    private void Enemy()
    {
        if (!onLight && !lightStatus)
        {
            deathTimer += Time.deltaTime;
        }
        else
        {
            deathTimer = 0;
            
        }
            

        if (deathTimer >= 5)
        {
            dead = true;
        }
    }

    private void UpdateText()
    {

        if (!dead)
        {
            if (lightStatus)
            {
                placeHolderText.text = "On";
            }
            else
            {
                placeHolderText.text = "Off";
            }
        }
        else
        {
            placeHolderText.text = "mueltooo";
        }
        

        placeHolderText2.text ="Carga: "+ dcCharge;
    }
    public void TurnLight()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lightStatus != true&&dcCharge>0)
            {
                lightStatus = true;
                UpdateText();
            }
            else
            {
                lightStatus = false;
                UpdateText();
            }
        }
    }
}
