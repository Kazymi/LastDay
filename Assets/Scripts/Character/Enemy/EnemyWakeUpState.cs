using StateMachine;

public class EnemyWakeUpState : State
{
    private readonly ICharacterAnimationController characterAnimationController;

    public EnemyWakeUpState(ICharacterAnimationController characterAnimationController)
    {
        this.characterAnimationController = characterAnimationController;
    }

    public override void OnStateEnter()
    {
        characterAnimationController.SetBool(CharacterAnimationType.WakeUP, true);
    }

    public override void OnStateExit()
    {
        characterAnimationController.SetBool(CharacterAnimationType.WakeUP, false);
    }
}