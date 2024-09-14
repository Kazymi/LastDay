public class CasualBullet : Bullet
{
    private ITraicerSpawner _traicerSpawner;

    protected override void Activate()
    {
        _traicerSpawner ??= ServiceLocator.GetService<ITraicerSpawner>();
        var damageTaker = ServiceLocator.GetService<IPlayerTargetSearcher>().FoundedTarget.target
            .GetComponent<IDamageTaker>();
        _traicerSpawner.SpawnTraicer(TraicerType.Bullet, damageTaker, damage, startPos, endPos);
        ServiceLocator.GetService<ISoundSystem>().PlaySound(SoundType.Ar);
    }
}