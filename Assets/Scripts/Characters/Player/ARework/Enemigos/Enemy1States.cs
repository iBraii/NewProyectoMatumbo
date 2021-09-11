using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy1States : MonoBehaviour
{
    private bool isFollowing;
    private bool canAttack;
    private bool path;
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

    void Start()
    {
        UpdatePathState(EnemyPathState.OnPath);
    }
    private void Update()
    {
        Triggered();
    }
    void Triggered()
    {
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject,1f))
        {
            UpdatePathState(EnemyPathState.Following);
        }
        else if((isFollowing && canAttack) && DetectPlayer.detection.CheckIfMoreDistance(this.gameObject, 1f))
        {
            UpdatePathState(EnemyPathState.Confused);
        }
        if(path)
        {
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
                case EnemyPathState.Confused:
                    HandleConfused();
                    break;
            }
        }
    }

    void HandlePath()
    {
        path = true;
        isFollowing = false;
        canAttack = false;
        Debug.Log("BUSCANDO A ED");
    }
    void HandleFollowing()
    {
        path = false;
        isFollowing = true;
        canAttack = true;
        Debug.Log("AHI TE CAIGO MRD");
    }
    void HandleConfused()
    {
        path = false;
        isFollowing = false;
        canAttack = false;
        Debug.Log("Confundido");
        StartCoroutine(FollowPath());
    }
    void HandleDenied()
    {
        if(DenyEnemy.inRange)
        {
            Debug.Log("ME DENIEGAN");
            path = false;
            isFollowing = false;
            canAttack = false;
        }
        else
        {
            UpdatePathState(EnemyPathState.Confused);
        }
    }
    IEnumerator FollowPath()
    {
        yield return new WaitForSeconds(3);
        HandlePath();
    }
}

public enum EnemyPathState
{
    OnPath,
    Following,
    Confused
}


