using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(TargetSearcher))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] protected ZombieHealthController zombieombieHealthController;
    [SerializeField] protected Animator animator;
    [SerializeField] protected EnemyConfiguration enemyConfiguration;

    protected TargetSearcher targetSearcher;
    protected NavMeshAgent navMeshAgent;
    protected global::StateMachine.StateMachine stateMachine;
    protected ICharacterAnimationController characterAnimationController;

    private void Start()
    {
        var scaleRandom = Random.Range(0.8f, 1.1f);
        transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
        OnInit();
    }

    private void OnEnable()
    {
        zombieombieHealthController.HealthEmpty += ZombieDead;
    }

    private void OnDisable()
    {
        zombieombieHealthController.HealthEmpty -= ZombieDead;
    }

    private void ZombieDead()
    {
        SaveData.Instance.Wallet.AddMoney(enemyConfiguration.AddMoney);
        stateMachine.SetState(new State());
    }

    private void OnInit()
    {
        targetSearcher = GetComponent<TargetSearcher>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimationController = new CharacterAnimationController(animator);
        InitializeStateMachine();
    }

    private void Update()
    {
        stateMachine.Tick();
        Tick();
    }

    protected virtual void Tick(){}
    private void InitializeStateMachine()
    {
        var idleState = new EnemyIdleState(characterAnimationController);
        var wakeUPState = new EnemyWakeUpState(characterAnimationController);
        var moveToTargetState =
            new EnemyMoveState(characterAnimationController, navMeshAgent, enemyConfiguration.Speed, targetSearcher);
        var attackState = new EnemyAttackState(characterAnimationController, transform, targetSearcher);

        attackState.AddTransition(new StateTransition(moveToTargetState,
            new FuncCondition(() => attackState.IsNearTarget == false)));

        idleState.AddTransition(new StateTransition(wakeUPState,
            new FuncCondition(() => targetSearcher.IsTargetFounded)));

        wakeUPState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(animator, CharacterAnimationType.WakeUP.ToString())));

        moveToTargetState.AddTransition(new StateTransition(attackState,
            new FuncCondition(() => moveToTargetState.IsNearTarget)));
        moveToTargetState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => targetSearcher.IsTargetFounded == false)));

        stateMachine = new global::StateMachine.StateMachine(idleState);
        StateMachineInitialized(idleState, wakeUPState, moveToTargetState, attackState);
    }

    protected virtual void StateMachineInitialized(State idle, State walkeUp, State moveToTarget, State attackState)
    {
    }
}