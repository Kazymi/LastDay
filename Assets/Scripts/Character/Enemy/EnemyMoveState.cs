using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : State
{
    private readonly ICharacterAnimationController characterAnimationController;
    private readonly NavMeshAgent navMeshAgent;
    private readonly EnemyConfiguration enemyConfiguration;
    private readonly ITargetSearcher targetSearcher;

    private const float nearDistance = 1;

    public bool IsNearTarget => targetSearcher.IsTargetFounded && Vector3.Distance(navMeshAgent.transform.position, targetSearcher.FoundedTarget.TargetPosition) < nearDistance;

    public EnemyMoveState(ICharacterAnimationController characterAnimationController, NavMeshAgent navMeshAgent,
        EnemyConfiguration enemyConfiguration, ITargetSearcher targetSearcher)
    {
        this.characterAnimationController = characterAnimationController;
        this.navMeshAgent = navMeshAgent;
        this.enemyConfiguration = enemyConfiguration;
        this.targetSearcher = targetSearcher;
    }

    public override void OnStateEnter()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.speed = enemyConfiguration.Speed;
        characterAnimationController.SetBool(CharacterAnimationType.Walk, true);
    }

    public override void OnStateExit()
    {
        navMeshAgent.enabled = false;
        navMeshAgent.speed = 0;
        characterAnimationController.SetBool(CharacterAnimationType.Walk, false);
    }

    public override void Tick()
    {
        if (targetSearcher.IsTargetFounded)
        {
            navMeshAgent.SetDestination(targetSearcher.FoundedTarget.TargetPosition);
        }
    }
}