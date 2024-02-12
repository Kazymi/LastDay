public class PlayerDamageTaker : DamageTaker,IPlayerDamageTarget
{
    private void OnEnable()
    {
        ServiceLocator.Subscribe<IPlayerDamageTarget>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IPlayerDamageTarget>();
    }
}

public interface IPlayerDamageTarget
{
    void TakeDamage(float damage);
}