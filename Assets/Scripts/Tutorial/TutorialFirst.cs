using System;
using EventBusSystem;

[Serializable]
public class TutorialFirst : TutorialInitializeClass, IJimSignal
{
    public override void OnUpdate()
    {
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.Joystic.gameObject.SetActive(false);
        tutorialParameters.JimCamera.gameObject.SetActive(true);

        tutorialParameters.CharacterAnimator.SetBool("IsTutorial", true);
        tutorialParameters.CharacterAnimator.SetBool("Ishide", true);
        EventBus.Subscribe(this);
    }

    protected override void OnComplete()
    {
        EventBus.Unsubscribe(this);
        tutorialParameters.Joystic.gameObject.SetActive(true);
        tutorialParameters.JimCamera.gameObject.SetActive(false);
        tutorialParameters.CharacterAnimator.SetBool("Ishide", false);
    }

    public void Finish()
    {
        Complete();
    }
}