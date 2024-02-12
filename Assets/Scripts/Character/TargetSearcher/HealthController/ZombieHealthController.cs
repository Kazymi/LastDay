using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ZombieHealthController : HealthController
{
    [SerializeField] private HealthHitChance headHitParameters;
    [SerializeField] private HealthHitChance bodyHitParameters;
    [SerializeField] private HealthHitChance armHitParameters;
    [SerializeField] private SearchTarget searchTarget;

    private IPlayerController playerController;
    private IEffectSpawner effectSpawner;
    private IDamagePopupSpawner popupSpawner;

    private bool isCanBespawenBlood = true;
    private bool isAlive = true;

    private void Start()
    {
        popupSpawner = ServiceLocator.GetService<IDamagePopupSpawner>();
        playerController = ServiceLocator.GetService<IPlayerController>();
        effectSpawner = ServiceLocator.GetService<IEffectSpawner>();
    }

    protected override void DamageReceived(float damage)
    {
        var chanceMax = 15;
        var randomHit = Random.Range(0, chanceMax);
        if (headHitParameters.Chance >= randomHit)
        {
            TakeHit(headHitParameters, damage, true);
            return;
        }

        if (bodyHitParameters.Chance >= randomHit)
        {
            StepBack();
            TakeHit(bodyHitParameters, damage, false);
            return;
        }

        TakeHit(armHitParameters, damage, false);
    }

    protected override void Dead()
    {
        base.Dead();
        if (isAlive)
        {
            isAlive = false;
            ServiceLocator.GetService<ILevelSystem>().DeadZombie++;
        }

        searchTarget.IsTargetAlive = false;
    }

    private void StepBack()
    {
        var stranght = 0.1f;
        var movepos = playerController.PlayerPosition - transform.position;
        movepos = movepos.normalized;
        movepos *= stranght;
        transform.DOMove(transform.position - movepos, 0.1f);
    }

    private void TakeHit(HealthHitChance healthHitChance, float damage, bool isCrit)
    {
        var key = Random.Range(0, healthHitChance.HealthHitConfiguration.HitObject.Length);
        if (healthHitChance.IsAnimated == false)
        {
            var hitModificator = 1;
            var takeAxys = healthHitChance.HealthHitConfiguration.HitObject[key].position -
                           playerController.PlayerPosition;
            var animateObject = healthHitChance.HealthHitConfiguration.HitObject[key];
            var startRotate = animateObject.localRotation;

            var rotate = animateObject.rotation.eulerAngles;
            rotate.z = rotate.z + healthHitChance.HealthHitConfiguration.ZRotate;
            animateObject.DORotate(rotate, 0.1f)
                .OnComplete(() => animateObject.DOLocalRotateQuaternion(startRotate, 0.1f));
            StartCoroutine(CooldownAnimate(healthHitChance));
        }

        if (isCanBespawenBlood)
        {
            effectSpawner.SpawnEffect(healthHitChance.HealthHitConfiguration.HitEffect,
                healthHitChance.HealthHitConfiguration.HitPosition[key]);
            StartCoroutine(SpawnEffect());
        }

        var damageFix = damage * healthHitChance.HealthHitConfiguration.DamageModificator;
        if (damageFix <= 1) damageFix = 1;
        popupSpawner.SpawnDamagePoput(transform.position, damageFix, isCrit);
        base.DamageReceived(damageFix);
    }

    private IEnumerator SpawnEffect()
    {
        isCanBespawenBlood = false;
        yield return new WaitForSeconds(0.1f);
        isCanBespawenBlood = true;
    }

    private IEnumerator CooldownAnimate(HealthHitChance healthHitChance)
    {
        healthHitChance.IsAnimated = true;
        yield return new WaitForSeconds(0.2f);
        healthHitChance.IsAnimated = false;
    }
}