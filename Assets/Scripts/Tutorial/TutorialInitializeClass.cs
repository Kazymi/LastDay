using UnityEngine;

public class TutorialInitializeClass : TutorialStep
{
    protected TutorialParameters tutorialParameters;

    public override void OnUpdate()
    {
    }

    protected override void OnBegin()
    {
        tutorialParameters = GameObject.FindObjectOfType<TutorialParameters>();
    }

    protected override void OnComplete()
    {
    }
}