using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : State
{
    private readonly ICharacterAnimationController characterAnimationController;
    private readonly NavMeshAgent navMeshAgent;
    private float speed;
    private readonly ITargetSearcher targetSearcher;
    private readonly CharacterAnimationType walkType;

    private const float nearDistance = 1;

    public bool IsNearTarget => targetSearcher.IsTargetFounded && Vector3.Distance(navMeshAgent.transform.position, targetSearcher.FoundedTarget.TargetPosition) < nearDistance;

    public EnemyMoveState(ICharacterAnimationController characterAnimationController, NavMeshAgent navMeshAgent,
        float speed, ITargetSearcher targetSearcher, CharacterAnimationType walkType = CharacterAnimationType.Walk)
    {
        this.characterAnimationController = characterAnimationController;
        this.navMeshAgent = navMeshAgent;
        this.speed = speed;
        this.targetSearcher = targetSearcher;
        this.walkType = walkType;
    }

    public void UpdateSpeed(float speed)
    {
        this.speed = speed;
    }
    public override void OnStateEnter()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.speed = speed;
        characterAnimationController.SetBool(walkType, true);
    }

    public override void OnStateExit()
    {
        navMeshAgent.enabled = false;
        navMeshAgent.speed = 0;
        characterAnimationController.SetBool(walkType, false);
    }

    public override void Tick()
    {
        if (targetSearcher.IsTargetFounded)
        {
            navMeshAgent.SetDestination(targetSearcher.FoundedTarget.TargetPosition);
        }
    }
}