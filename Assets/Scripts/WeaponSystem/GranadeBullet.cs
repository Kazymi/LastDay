using System.Linq;
using UnityEngine;

public class GranadeBullet : Bullet
{
    private IEffectSpawner effectSpawner;

    protected override void Activate()
    {
        var target = ServiceLocator.GetService<IPlayerTargetSearcher>().FoundedTarget.target;
        if (target == null) return;
        var damageTaker = target.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(damage);
        effectSpawner ??= ServiceLocator.GetService<IEffectSpawner>();
        effectSpawner.SpawnEffect(EffectType.ExplosionMini, target);
        LayerMask layerMask = new LayerMask();
        layerMask.value = 128;
        var targetables = Physics.OverlapSphere(target.position, 2, layerMask)
            .Where(t => t.GetComponent<IDamageTaker>() != null)
            .ToList();
        foreach (var targetable in targetables)
        {
            var takerDamage = targetable.GetComponent<IDamageTaker>();
            if(takerDamage == damageTaker) continue;
            takerDamage.TakeDamage(damage/2);
        }
    }
}