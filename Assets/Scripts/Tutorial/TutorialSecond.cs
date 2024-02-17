using System;
using UnityEngine;

[Serializable]
public class TutorialSecond : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (tutorialParameters.Joystic.Direction != Vector2.zero)
        {
            tutorialParameters.WASDController.gameObject.SetActive(false);
            tutorialParameters.FingerController.gameObject.SetActive(false);
        }

        if (tutorialParameters.FindweaponQuest.IsQuestCompleted()) Complete();
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.WASDController.gameObject.SetActive(true);
        tutorialParameters.FingerController.gameObject.SetActive(true);
        tutorialParameters.QuestStateMachine.StartNewQuest(tutorialParameters.FindweaponQuest);
        GameObject.FindObjectOfType<TutorialWeapon>().Outline.enabled = true;
    }

    protected override void OnComplete()
    {
        GameObject.Destroy(GameObject.FindObjectOfType<TutorialWeapon>().gameObject);
        tutorialParameters.CharacterAnimator.SetBool("IsTutorial", false);
    }
}

[Serializable]
public class TutorialThirdStep : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (tutorialParameters.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f &&
            tutorialParameters.CharacterAnimator
                .GetCurrentAnimatorStateInfo(0)
                .IsName("TakeItem"))
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.CharacterAnimator.SetTrigger("TakeItem");
        tutorialParameters.Joystic.IsPause = true;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        tutorialParameters.Joystic.IsPause = false;
        tutorialParameters.WeaponMain.gameObject.SetActive(true);
    }
}

[Serializable]
public class TutorialFourthStep : TutorialInitializeClass
{
    private float currentTime = 2f;

    protected override void OnBegin()
    {
        base.OnBegin();
        currentTime = 2f;
        tutorialParameters.Levelsystem.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            Complete();
        }
    }
}

[Serializable]
public class TutorialFiveStep : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        if (tutorialParameters.KillZobies.IsQuestCompleted()) Complete();
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.QuestStateMachine.StartNewQuest(tutorialParameters.KillZobies);
    }
}