using UnityEngine;
using UnityEngine.AI;

public class EnemyPath : MonoBehaviour
{
    private bool isFollowing;
    public EnemyPathState state;
    private NavMeshAgent agent;
    private void Awake()
    {

    }
    void FollowPath()
    {

    }
    private void Update()
    {
        FollowTrigger();
    }
    void FollowTrigger()
    {
        if (DetectPlayer.detection.CheckIfLessDistance(this.gameObject,.5f))
            isFollowing = true;
        else
            isFollowing = false;
    }
    void UpdatePathState(EnemyPathState newState)
    {
        state = newState;

        switch(newState)
        {
            case EnemyPathState.OnPath:
                break;
            case EnemyPathState.Following:
                break;
            case EnemyPathState.Denied:
                break;
        }
    }
}

public enum EnemyPathState
{
    OnPath,
    Following,
    Denied
}


