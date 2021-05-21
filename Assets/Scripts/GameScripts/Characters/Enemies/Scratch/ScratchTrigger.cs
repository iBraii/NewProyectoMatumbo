using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchTrigger : MonoBehaviour
{
    public string playerTag;
    private EnemiesAttack sc_enemiesAtk;
    public float stressDamage;
    // Start is called before the first frame update
    void Start()
    {
        sc_enemiesAtk = GetComponent<EnemiesAttack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            sc_enemiesAtk.SpectresAttack(stressDamage);
            Debug.Log("Hola");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            sc_enemiesAtk.SpectresAttack(stressDamage);
            Debug.Log("Holass");
        }
    }
}
