using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ZombieHealthController : HealthController
{
    [SerializeField] private HealthHitChance headHitParameters;
    [SerializeField] private HealthHitChance bodyHitParameters;
    [SerializeField] private HealthHitChance armHitParameters;

    private IPlayerController playerController;
    private IEffectSpawner effectSpawner;

    private void Start()
    {
        playerController = ServiceLocator.GetService<IPlayerController>();
        effectSpawner = ServiceLocator.GetService<IEffectSpawner>();
    }

    protected override void DamageReceived(float damage)
    {
        var chanceMax = 15;
        var randomHit = Random.Range(0, chanceMax);
        if (headHitParameters.Chance >= randomHit)
        {
            TakeHit(headHitParameters, damage);
            return;
        }

        if (bodyHitParameters.Chance >= randomHit)
        {
            TakeHit(bodyHitParameters, damage);
            return;
        }

        TakeHit(armHitParameters, damage);
    }

    private void TakeHit(HealthHitChance healthHitChance, float damage)
    {
        var key = Random.Range(0, healthHitChance.HealthHitConfiguration.HitObject.Length);
        if (healthHitChance.IsAnimated == false)
        {
            var hitModificator = 1;
            var takeAxys = healthHitChance.HealthHitConfiguration.HitObject[key].position - playerController.PlayerPosition;
            var animateObject = healthHitChance.HealthHitConfiguration.HitObject[key];
            var startRotate = animateObject.localRotation;

            var rotate = animateObject.rotation.eulerAngles;
            rotate.z = rotate.z + healthHitChance.HealthHitConfiguration.ZRotate;
            animateObject.DORotate(rotate, 0.1f)
                .OnComplete(() => animateObject.DOLocalRotateQuaternion(startRotate, 0.1f));
            StartCoroutine(CooldownAnimate(healthHitChance));
        }

        effectSpawner.SpawnEffect(healthHitChance.HealthHitConfiguration.HitEffect,
            healthHitChance.HealthHitConfiguration.HitPosition[key]);
        base.DamageReceived(damage * healthHitChance.HealthHitConfiguration.DamageModificator);
    }

    private IEnumerator CooldownAnimate(HealthHitChance healthHitChance)
    {
        healthHitChance.IsAnimated = true;
        yield return new WaitForSeconds(0.1f);
        healthHitChance.IsAnimated = false;
    }
}