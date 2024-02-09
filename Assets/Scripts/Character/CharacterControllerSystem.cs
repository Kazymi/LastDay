using StateMachine;
using StateMachine.Conditions;
using UnityEngine;

public class CharacterControllerSystem : MonoBehaviour, IPlayerController
{
    [SerializeField] private Transform characterBody;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private CharacterConfigurations characterConfigurations;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Joystick joystick;

    private CharacterAnimationController m_animationController;
    private StateMachine.StateMachine m_stateMachine;

    public Vector3 PlayerPosition => transform.position;

    private void Awake()
    {
        m_animationController = new CharacterAnimationController(characterAnimator);
        OnInit();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IPlayerController>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IPlayerController>();
    }

    private void OnInit()
    {
        InitializeStateMachine();
    }

    private void Update()
    {
        m_stateMachine.FixedTick();
    }

    private void FixedUpdate()
    {
        m_stateMachine.Tick();
    }

    private void InitializeStateMachine()
    {
        var moveState = new CharacterMoveState(rigidbody, characterConfigurations, joystick, m_animationController,
            characterBody);
        var idleState = new CharacterIdleState(m_animationController);

        //idleState transitions
        idleState.AddTransition(new StateTransition(moveState,
            new FuncCondition(() => joystick.Direction != Vector2.zero)));

        //moveState transitions
        moveState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => joystick.Direction == Vector2.zero)));


        m_stateMachine = new StateMachine.StateMachine(idleState);
    }
}