using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/TutorialQuestKillZombie",
    fileName = "TutorialQuestKillZombie",
    order = 0)]
public class TutorialQuestKillZombie : Quest
{
    private ILevelSystem levelSystem;
    private IZombieSpawner zombieSpawner;

    public override float GetCurrentProgress()
    {
        return (float) levelSystem.DeadZombie / (float) zombieSpawner.CurrentZombieAmount;
    }

    public override string GetDescriptionProgress()
    {
        levelSystem = ServiceLocator.GetService<ILevelSystem>();
        zombieSpawner = ServiceLocator.GetService<IZombieSpawner>();

        return $"Destroy the infected! {levelSystem.DeadZombie}/{zombieSpawner.CurrentZombieAmount}";
    }

    public override string GetMessage()
    {
        levelSystem = ServiceLocator.GetService<ILevelSystem>();
        zombieSpawner = ServiceLocator.GetService<IZombieSpawner>();

        return $"Destroy the infected! {levelSystem.DeadZombie}/{zombieSpawner.CurrentZombieAmount}";
    }

    public override Sprite GetImage()
    {
        return ServiceLocator.GetService<ISpriteContainer>().GetSprite(SpriteType.ZombieFace);
    }

    public override bool IsQuestCompleted()
    {
        return levelSystem.DeadZombie == zombieSpawner.CurrentZombieAmount;
    }
}