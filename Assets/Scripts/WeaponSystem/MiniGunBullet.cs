using System.Linq;
using UnityEngine;

public class MiniGunBullet : Bullet
{
    protected override void Activate()
    {
        var target = ServiceLocator.GetService<IPlayerTargetSearcher>().FoundedTarget.target;
        LayerMask layerMask = new LayerMask();
        layerMask.value = 128;
        var targetable = Physics.OverlapSphere(target.position, 2,layerMask).Where(t => t.GetComponent<IDamageTaker>() != null)
            .ToList();
        for (int i = 0; i < 2; i++)
        {
            if (targetable.Count == 0) break;
            targetable[Random.Range(0, targetable.Count)].GetComponent<IDamageTaker>().TakeDamage(damage);
        }
        ServiceLocator.GetService<ISoundSystem>().PlaySound(SoundType.Ar);
    }
}