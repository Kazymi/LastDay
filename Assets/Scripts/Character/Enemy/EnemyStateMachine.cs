using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(TargetSearcher))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private ZombieHealthController zombieombieHealthController;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyConfiguration enemyConfiguration;

    private TargetSearcher targetSearcher;
    private NavMeshAgent navMeshAgent;
    private global::StateMachine.StateMachine stateMachine;
    private ICharacterAnimationController characterAnimationController;

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
    }

    private void InitializeStateMachine()
    {
        var idleState = new EnemyIdleState(characterAnimationController);
        var wakeUPState = new EnemyWakeUpState(characterAnimationController);
        var moveToTargetState =
            new EnemyMoveState(characterAnimationController, navMeshAgent, enemyConfiguration, targetSearcher);
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
    }
}