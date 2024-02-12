using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour, IBulletSpawner
{
    private Dictionary<BulletType, Bullet> effects =
        new Dictionary<BulletType, Bullet>();

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IBulletSpawner>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IBulletSpawner>();
    }

    public void SpawnBullet(BulletType effectType, float damage)
    {
        TryToInitializeEffect(effectType);
        var newEffect = effects[effectType];
        newEffect.Setup(damage);
    }

    private void TryToInitializeEffect(BulletType effectType)
    {
        if (effects.ContainsKey(effectType))
        {
            return;
        }

        switch (effectType)
        {
            case BulletType.ARBullets:
                effects.Add(BulletType.ARBullets, new CasualBullet());
                break;
            case BulletType.ShootGun:
                effects.Add(BulletType.ShootGun, new ShotGunBullet());
                break;
            case BulletType.Granade:
                effects.Add(BulletType.Granade, new GranadeBullet());
                break;
            case BulletType.Minigun:
                effects.Add(BulletType.Minigun, new MiniGunBullet());
                break;
        }
    }
}

public interface IBulletSpawner
{
    void SpawnBullet(BulletType effectType, float damage);
}

[Serializable]
public class BulletConfiguration
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private Bullet bullet;

    public BulletType BulletType => bulletType;

    public Bullet Bullet => bullet;
}