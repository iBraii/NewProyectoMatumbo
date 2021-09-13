using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    private PlayerM sc_playerM;
    public Enemies sc_enemies;
    void Start()
    {
        sc_playerM = GameObject.Find("Player").GetComponent<PlayerM>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DoShake();
        }
    }
    public void SpectresAttack(float stressAmountValue)
    {
        if(sc_playerM.isInmune == false && sc_enemies.isDenied == false)
        {
            sc_playerM.life += stressAmountValue * Time.deltaTime;
        }  
    }
    public void AurasDamage(float stressAmountValue)
    {
        sc_playerM.life += stressAmountValue * Time.deltaTime;
    }
    void DoShake()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        Noise(3,3);
        yield return new WaitForSeconds(1);
        Noise(.5f,.5f);
        
    }
    void Noise(float amplitude, float frequency)
    {
        sc_playerM.screenShake.m_AmplitudeGain = amplitude;
        sc_playerM.screenShake.m_FrequencyGain = frequency;
    }
}
