using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamTrigger : MonoBehaviour
{
    public string playerTag;
    private EnemiesAttack sc_enemiesAtk;
    private ObjectM sc_ObjectM;
    public int stressDamage;
    // Start is called before the first frame update
    void Start()
    {
        sc_enemiesAtk = GetComponent<EnemiesAttack>();
        sc_ObjectM = GameObject.Find("Enemigo2").GetComponent<ObjectM>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(sc_ObjectM.blink == true && sc_ObjectM.isDenied == false)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                sc_enemiesAtk.SpectresAttack(stressDamage);
            }
        }  
    }
    private void OnTriggerStay(Collider other)
    {
        if (sc_ObjectM.blink == true && sc_ObjectM.isDenied == false)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                sc_enemiesAtk.SpectresAttack(stressDamage);
            }
        }
    }
}
