using NaughtyAttributes;

public class PlayerHealthBase : HealthController
{
    protected override void DamageReceived(float damage)
    {
        if (SaveData.Instance.lastHit == false)
        {
            if (SaveData.Instance.IsInvinsible) return;
        }

        base.DamageReceived(damage);
    }
}