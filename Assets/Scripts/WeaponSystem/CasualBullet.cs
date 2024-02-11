public class CasualBullet : Bullet
{
    protected override void Activate()
    {
        var damageTaker = ServiceLocator.GetService<IPlayerTargetSearcher>().FoundedTarget.target
            .GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(damage);
    }
}