using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    private PlayerM sc_playerM;
    private float stressAugmentCD;
    private bool isCDup;
    public float atkRate;
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
        if(isCDup)
        {
            stressAugmentCD += Time.deltaTime;
            if(stressAugmentCD >= atkRate)
            {
                isCDup = false;
                stressAugmentCD = 0;
            }
        }
    }
    public void SpectresAttack(float stressAmountValue)
    {
        if(sc_playerM.isInmune == false)
        {
            if (isCDup == false)
            {
                sc_playerM.life += stressAmountValue;
                isCDup = true;
            }
        }  
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
