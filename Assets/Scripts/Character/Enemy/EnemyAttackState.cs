using DG.Tweening;
using StateMachine;
using UnityEngine;

public class EnemyAttackState : State
{
    private readonly ICharacterAnimationController characterAnimationController;
    private readonly Transform enemy;
    private readonly ITargetSearcher targetSearcher;
    private const float nearDistance = 1;

    public bool IsNearTarget =>
        Vector3.Distance(enemy.position, targetSearcher.FoundedTarget.TargetPosition) < nearDistance;

    public EnemyAttackState(ICharacterAnimationController characterAnimationController, Transform enemy,
        ITargetSearcher targetSearcher)
    {
        this.characterAnimationController = characterAnimationController;
        this.enemy = enemy;
        this.targetSearcher = targetSearcher;
    }

    public override void OnStateEnter()
    {
        characterAnimationController.SetBool(CharacterAnimationType.ZombieAttack, true);
        enemy.transform.DOKill();
        var lookAtPos = targetSearcher.FoundedTarget.target.position;
        lookAtPos.y = enemy.position.y;
        enemy.transform.DOLookAt(lookAtPos, 0.7f);
    }

    public override void OnStateExit()
    {
        characterAnimationController.SetBool(CharacterAnimationType.ZombieAttack, false);
        enemy.transform.DOKill();
    }
}