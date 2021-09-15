using UnityEngine;
using System;

public class DamageFunction : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            DamagePlayer(damage);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            DamagePlayer(damage);
    }
    void DamagePlayer(float damage)
    {
        PlayerSingleton.Instance.stress += damage * Time.deltaTime;
    }
}
