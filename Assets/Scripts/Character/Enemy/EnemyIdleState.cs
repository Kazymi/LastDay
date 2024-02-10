using StateMachine;

public class EnemyIdleState : State
{
    private readonly ICharacterAnimationController characterAnimationController;

    public EnemyIdleState(ICharacterAnimationController characterAnimationController)
    {
        this.characterAnimationController = characterAnimationController;
    }

    public override void OnStateEnter()
    {
        characterAnimationController.SetBool(CharacterAnimationType.Idle, true);
    }

    public override void OnStateExit()
    {
        characterAnimationController.SetBool(CharacterAnimationType.Idle, false);
    }
}