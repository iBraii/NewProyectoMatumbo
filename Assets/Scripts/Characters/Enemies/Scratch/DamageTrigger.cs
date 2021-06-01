using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public string playerTag;
    private EnemiesAttack sc_enemiesAtk;
    public float damageValue;
    // Start is called before the first frame update
    void Start()
    {
        sc_enemiesAtk = GetComponent<EnemiesAttack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            sc_enemiesAtk.SpectresAttack(damageValue);
            Debug.Log("Hola");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            sc_enemiesAtk.SpectresAttack(damageValue);
            Debug.Log("Holass");
        }
    }
}
