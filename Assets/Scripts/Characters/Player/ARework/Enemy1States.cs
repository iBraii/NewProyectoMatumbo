using UnityEngine;
using UnityEngine.AI;

public class Enemy1States : MonoBehaviour
{
    private bool isFollowing;
    private bool canAttack;
    private NavMeshAgent agent;
    public EnemyPathState state;

    private void Awake()
    {
        Dreams.onWeaponUsed += HandleDenied;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= HandleDenied;
    }
    private void Update()
    {
        Triggered();
    }
    void Triggered()
    {
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject,.5f))
        {
            isFollowing = true;
            canAttack = true;
            UpdatePathState(EnemyPathState.Following);
        }
        else
        {
            isFollowing = false;
            canAttack = false;
            UpdatePathState(EnemyPathState.OnPath);
        }
    }
    void UpdatePathState(EnemyPathState newState)
    {
        if (DenyEnemy.inRange == false)
        {
            state = newState;
            switch (newState)
            {
                case EnemyPathState.OnPath:
                    HandlePath();
                    break;
                case EnemyPathState.Following:
                    HandleFollowing();
                    break;
            }
        }
    }
    void HandlePath()
    {
        Debug.Log("BUSCANDO A ED");
    }
    void HandleFollowing()
    {
        Debug.Log("AHI TE CAIGO MRD");
    }
    void HandleDenied()
    {
        if(DenyEnemy.inRange)
            Debug.Log("ME DENIEGAN");
    }
}

public enum EnemyPathState
{
    OnPath,
    Following
}


