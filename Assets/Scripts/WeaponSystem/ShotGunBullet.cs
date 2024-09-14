using System.Linq;
using UnityEngine;

public class ShotGunBullet : Bullet
{
    private ITraicerSpawner _traicerSpawner;

    protected override void Activate()
    {
        _traicerSpawner = ServiceLocator.GetService<ITraicerSpawner>();
        var target = ServiceLocator.GetService<IPlayerTargetSearcher>().FoundedTarget.target;
        var damageTaker = target.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(damage);
        LayerMask layerMask = new LayerMask();
        layerMask.value = 128;
        var targetable = Physics.OverlapSphere(target.position, 2, layerMask)
            .Where(t => t.GetComponent<IDamageTaker>() != null)
            .ToList();
        for (int i = 0; i < 6; i++)
        {
            if (targetable.Count == 0) break;

            var randomRange = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
            var idamageTaker = targetable[Random.Range(0, targetable.Count)].GetComponent<IDamageTaker>();
            _traicerSpawner.SpawnTraicer(TraicerType.Shotgun, idamageTaker, damage, startPos,
                damageTaker.takerPosition + randomRange);
        }

        ServiceLocator.GetService<ISoundSystem>().PlaySound(SoundType.ShotGun);
    }
}