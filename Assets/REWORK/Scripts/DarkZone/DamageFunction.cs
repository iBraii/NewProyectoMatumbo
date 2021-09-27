using UnityEngine;

public class DamageFunction : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(damage);
            PostProfileManager.Instance.ChangeProfile(1);
            PlayerSingleton.Instance.beingAttacked = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(damage);
            PlayerSingleton.Instance.beingAttacked = true;
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PostProfileManager.Instance.ChangeProfile(0);
            PlayerSingleton.Instance.beingAttacked = false;
        }
    }       
    void DamagePlayer(float damage)
    {
        PlayerSingleton.Instance.stress += damage * Time.deltaTime;
    }
}
