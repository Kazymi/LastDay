using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/TutorialQuestFinishTutorial",
    fileName = "AttachConfiguration",
    order = 0)]
public class TutorialQuestFinishTutorial : Quest
{
    private TutorialExit tutorialExit;
    private IPlayerController playerController;

    public override float GetCurrentProgress()
    {
        return 0;
    }

    public override string GetDescriptionProgress()
    {
        return "Follow the survivor out of town";
    }

    public override string GetMessage()
    {
        return "Follow the survivor out of town";
    }

    public override Sprite GetImage()
    {
        return ServiceLocator.GetService<ISpriteContainer>().GetSprite(SpriteType.TutorialExit);
    }

    public override bool IsQuestCompleted()
    {
        tutorialExit = FindObjectOfType<TutorialExit>();
        playerController = ServiceLocator.GetService<IPlayerController>();
        var distance = 2f;
        return Vector3.Distance(playerController.PlayerPosition, tutorialExit.transform.position) < distance;
    }
}