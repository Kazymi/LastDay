using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/TutorialQuestFindWeapon",
    fileName = "AttachConfiguration",
    order = 0)]
public class TutorialQuestFindWeapon : Quest
{
    private TutorialWeapon tutorialWeapon;
    private IPlayerController playerController;

    public override float GetCurrentProgress()
    {
        return 0;
    }

    public override string GetDescriptionProgress()
    {
        return "Pick up a weapon";
    }

    public override string GetMessage()
    {
        return "Pick up a weapon";
    }

    public override Sprite GetImage()
    {
        return ServiceLocator.GetService<ISpriteContainer>().GetSprite(SpriteType.ZombieFace);
    }

    public override bool IsQuestCompleted()
    {
        tutorialWeapon = FindObjectOfType<TutorialWeapon>();
        playerController = ServiceLocator.GetService<IPlayerController>();
        var distance = 2f;
        return Vector3.Distance(playerController.PlayerPosition, tutorialWeapon.WeaponPosition) < distance;
    }
}