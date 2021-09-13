using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public string playerTag;
    private EnemiesAttack sc_enemiesAtk;
    public float enemiesDamageValue, aurasDamageValue;
    // Start is called before the first frame update
    void Start()
    {
        sc_enemiesAtk = GetComponent<EnemiesAttack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            if(sc_enemiesAtk.sc_enemies != null)
            {
                sc_enemiesAtk.SpectresAttack(enemiesDamageValue);
                other.gameObject.GetComponent<PlayerM>().cooldownForStressRegen = 0;
            }
            sc_enemiesAtk.AurasDamage(aurasDamageValue);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (sc_enemiesAtk.sc_enemies != null)
            {
                sc_enemiesAtk.SpectresAttack(enemiesDamageValue);
                other.gameObject.GetComponent<PlayerM>().cooldownForStressRegen = 0;
            }
            sc_enemiesAtk.AurasDamage(aurasDamageValue);
        }
    }
}
