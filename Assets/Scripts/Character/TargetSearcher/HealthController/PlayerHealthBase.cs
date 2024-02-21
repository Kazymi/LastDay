using NaughtyAttributes;

public class PlayerHealthBase : HealthController
{
    [Button("Take testDamage")]
    private void TakeDamage()
    {
        DamageReceived(5);
    }
}

