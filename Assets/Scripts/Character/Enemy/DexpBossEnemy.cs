using System.Collections;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class DexpBossEnemy : EnemyStateMachine
{
    [Space] [Header("Boss setting")] [SerializeField]
    private float radius;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform attackCenter;
    [SerializeField] private int damage;

    private bool isCanBeDexpAttack = true;
    private bool isAgressive = false;

    private EnemyWakeUpState wakeUPState;
    private EnemyMoveState moveToTargetState;

    protected override void StateMachineInitialized(State idle, State walkeUp, State moveToTarget, State attackState)
    {
        var idleState = new EnemyIdleState(characterAnimationController);
        wakeUPState = new EnemyWakeUpState(characterAnimationController);

        moveToTargetState =
            new EnemyMoveState(characterAnimationController, navMeshAgent, enemyConfiguration.Speed, targetSearcher);

        var newAttackState = new EnemyAttackState(characterAnimationController, transform, targetSearcher);

        newAttackState.AddTransition(new StateTransition(moveToTargetState,
            new TemporaryAndFuncCondition(() => newAttackState.IsNearTarget == false, 0.3f)));

        idleState.AddTransition(new StateTransition(wakeUPState,
            new FuncCondition(() => targetSearcher.IsTargetFounded)));

        wakeUPState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(animator, CharacterAnimationType.WakeUP.ToString())));
        wakeUPState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(animator, CharacterAnimationType.WakeUpSecond.ToString())));

        moveToTargetState.AddTransition(new StateTransition(newAttackState,
            new FuncCondition(() => moveToTargetState.IsNearTarget)));
        moveToTargetState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => targetSearcher.IsTargetFounded == false)));


        stateMachine = new global::StateMachine.StateMachine(idleState);
        var BloodAttackState =
            new DexpAttackState(attackCenter, radius, damage, playerMask, characterAnimationController, this);
        var dexpAttackPrepare = new State();

        moveToTargetState.AddTransition(new StateTransition(dexpAttackPrepare, new FuncCondition(() =>
        {
            if (isCanBeDexpAttack == false) return false;
            var distance = Vector3.Distance(transform.position,
                ServiceLocator.GetService<IPlayerController>().PlayerPosition) < 5;
            return distance;
        })));
        dexpAttackPrepare.AddTransition(new StateTransition(BloodAttackState, new FuncCondition(() => true)));

        BloodAttackState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(characterAnimationController.Animator,
                CharacterAnimationType.BossAttack.ToString())));
        BloodAttackState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(characterAnimationController.Animator,
                CharacterAnimationType.BossAttackSecond.ToString())));

        stateMachine = new global::StateMachine.StateMachine(idleState);
    }

    protected override void Tick()
    {
        if (isAgressive == false)
            if (zombieombieHealthController.currentDamagePerce < 0.5f)
            {
                InitializeAgr();
            }
    }

    private void InitializeAgr()
    {
        isAgressive = true;
        ServiceLocator.GetService<ISoundSystem>().PlaySound(SoundType.DexpScream);
        moveToTargetState.UpdateSpeed(enemyConfiguration.Speed + enemyConfiguration.Speed * 0.1f);
        characterAnimationController.SetPlay(CharacterAnimationType.WakeUpSecond, true);
        stateMachine.SetState(wakeUPState);
    }

    public void DexpAttackCooldown()
    {
        StartCoroutine(DexpCooldown());
    }

    private IEnumerator DexpCooldown()
    {
        isCanBeDexpAttack = false;
        yield return new WaitForSeconds(10f);
        isCanBeDexpAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackCenter == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter.position, radius);
    }
}