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
    protected EnemyFallState enemyFallState;
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

    public void ToFall()
    {
        Debug.Log("ToFall");
        stateMachine.SetState(enemyFallState);
    }

    private void OnDisable()
    {
        zombieombieHealthController.HealthEmpty -= ZombieDead;
    }

    private void ZombieDead()
    {
        navMeshAgent.enabled = false;
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

    protected virtual void Tick()
    {
    }

    private void InitializeStateMachine()
    {
        var idleState = new EnemyIdleState(characterAnimationController);
        var speed = Random.Range(enemyConfiguration.Speed * 0.8f, enemyConfiguration.Speed * 1.3f);
        var moveToTargetState =
            new EnemyMoveState(characterAnimationController, navMeshAgent, speed, targetSearcher);
        var attackState = new EnemyAttackState(characterAnimationController, transform, targetSearcher);
        enemyFallState = new EnemyFallState(characterAnimationController, navMeshAgent, speed);
        var enemyStandUpAfterFallState = new StandUpAfterFallState(characterAnimationController);

        enemyFallState.AddTransition(new StateTransition(enemyStandUpAfterFallState, new TemporaryCondition(0.6f)));
        enemyStandUpAfterFallState.AddTransition(new StateTransition(moveToTargetState,
            new AnimationFinishCondition(characterAnimationController.Animator,
                CharacterAnimationType.WakeUpAfterFall.ToString())));

        attackState.AddTransition(new StateTransition(moveToTargetState,
            new FuncCondition(() => attackState.IsNearTarget == false)));

        moveToTargetState.AddTransition(new StateTransition(attackState,
            new FuncCondition(() => moveToTargetState.IsNearTarget)));
        moveToTargetState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => targetSearcher.IsTargetFounded == false)));

        idleState.AddTransition(new StateTransition(moveToTargetState,
            new FuncCondition(() => targetSearcher.IsTargetFounded)));

        stateMachine = new global::StateMachine.StateMachine(idleState);
        StateMachineInitialized(idleState, idleState, moveToTargetState, attackState);
    }

    protected virtual void StateMachineInitialized(State idle, State walkeUp, State moveToTarget, State attackState)
    {
    }
}

public class EnemyFallState : State
{
    private readonly ICharacterAnimationController _characterAnimationController;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly float speed;

    public EnemyFallState(ICharacterAnimationController characterAnimationController, NavMeshAgent navMeshAgent,
        float speed)
    {
        _characterAnimationController = characterAnimationController;
        _navMeshAgent = navMeshAgent;
        this.speed = speed;
    }

    public override void OnStateEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = speed;
        _characterAnimationController.SetPlay(CharacterAnimationType.Fall, true);
    }
}

public class StandUpAfterFallState : State
{
    private readonly ICharacterAnimationController _characterAnimationController;

    public StandUpAfterFallState(ICharacterAnimationController characterAnimationController)
    {
        _characterAnimationController = characterAnimationController;
    }

    public override void OnStateEnter()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.WakeUpAfterFall, true);
    }

    public override void OnStateExit()
    {
        _characterAnimationController.SetBool(CharacterAnimationType.WakeUpAfterFall, false);
    }
}