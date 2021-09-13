using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamTrigger : MonoBehaviour
{
    public string playerTag;
    private EnemiesAttack sc_enemiesAtk;
    [SerializeField] private ObjectM sc_ObjectM;
    public int stressDamage;
    // Start is called before the first frame update
    void Start()
    {
        sc_enemiesAtk = GetComponent<EnemiesAttack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (sc_ObjectM.blink == true && sc_ObjectM.isDenied == false)
            {
                sc_enemiesAtk.SpectresAttack(stressDamage);
            }
        }  
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (sc_ObjectM.blink == true && sc_ObjectM.isDenied == false)
            {
                sc_enemiesAtk.SpectresAttack(stressDamage);
            }
        }
    }
}
