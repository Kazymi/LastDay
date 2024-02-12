using DG.Tweening;
using StateMachine;
using UnityEngine;

public class EnemyAttackState : State
{
    private readonly ICharacterAnimationController characterAnimationController;
    private readonly Transform enemy;
    private readonly ITargetSearcher targetSearcher;
    private const float nearDistance = 1.8f;

    public bool IsNearTarget =>
        Vector3.Distance(enemy.position, targetSearcher.FoundedTarget.TargetPosition) < nearDistance;

    private const float attackTimer = 0.7f;
    private float currentTime;

    public EnemyAttackState(ICharacterAnimationController characterAnimationController, Transform enemy,
        ITargetSearcher targetSearcher)
    {
        this.characterAnimationController = characterAnimationController;
        this.enemy = enemy;
        this.targetSearcher = targetSearcher;
    }

    public override void Tick()
    {
        if(currentTime <= 0) Attack();
        currentTime -= Time.deltaTime;
    }

    private void Attack()
    {
        currentTime = attackTimer;
        ServiceLocator.GetService<IPlayerDamageTarget>().TakeDamage(5);
    }

    public override void OnStateEnter()
    {
        currentTime = attackTimer;
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