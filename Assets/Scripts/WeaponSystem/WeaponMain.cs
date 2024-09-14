using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponMain : MonoBehaviour, IWeaponMain
{
    [SerializeField] private AttachmentConfigurationConstructor attachmentConfigurationConstructor;
    [SerializeField] private PlayerHealthBase playerHealthBase;
    [SerializeField] private bool readyToShot;
    private WeaponConfiguration weaponConfiguration;
    private WeaponEffectSpawner _weaponEffectSpawner;

    private bool isReload;
    private bool isCanBeShot;
    public bool CanBeShot => isCanBeShot && readyToShot && isReload == false;

    private float shotCooldown;

    private IBulletSpawner bulletSpawner;
    private IPlayerTargetSearcher targetSearcher;
    private IPlayerAnimatorController animatorController;

    public Dictionary<WeaponConfiguration, BulletStorage> amountBullet =
        new Dictionary<WeaponConfiguration, BulletStorage>();

    private Dictionary<ParametersType, float> fixValue;

    public WeaponType CurrentType { get; private set; }
    public WeaponConfiguration WeaponConfiguration => weaponConfiguration;
    public float CritChance => GetFixParameters(ParametersType.CritChance);
    public event Action Shoted;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IWeaponMain>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IWeaponMain>();
    }

    private void Start()
    {
        playerHealthBase.HealthEmpty += () => readyToShot = false;
        OnInit();
    }

    private void Update()
    {
        Tick();
        ShotCooldown();
        ReadyShotCheck();
        if (Input.GetKeyDown(KeyCode.R) && isReload == false) StartCoroutine(Reload(weaponConfiguration));
    }

    public void Initialize(WeaponConfiguration weaponConfiguration)
    {
        this.weaponConfiguration = weaponConfiguration;
        GenerateParametersWithParameters();
        if (amountBullet.ContainsKey(weaponConfiguration) == false)
        {
            amountBullet.Add(weaponConfiguration,
                new BulletStorage()
                    {AmountBullet = weaponConfiguration.AmounBullet, MaxBullet = weaponConfiguration.AmounBullet * 8});
        }
    }

    private void ReadyShotCheck()
    {
        animatorController.SetBool(CharacterAnimationType.IsTargetInZone, CanBeShot);
    }

    private void OnInit()
    {
        targetSearcher = ServiceLocator.GetService<IPlayerTargetSearcher>();
        bulletSpawner = ServiceLocator.GetService<IBulletSpawner>();
        animatorController = ServiceLocator.GetService<IPlayerAnimatorController>();
        animatorController.SetBool(CharacterAnimationType.Weapon, true);
    }

    private void ShotCooldown()
    {
        if (CanBeShot == false)
        {
            return;
        }

        if (weaponConfiguration == null) return;
        if (amountBullet.ContainsKey(weaponConfiguration) && amountBullet[weaponConfiguration].AmountBullet == 0)
        {
            if (isReload == false) StartCoroutine(Reload(weaponConfiguration));
            return;
        }

        if (shotCooldown <= 0)
        {
            Shoot();
            amountBullet[weaponConfiguration].AmountBullet--;
            shotCooldown = GetFixParameters(ParametersType.FireRate);
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }

        if (amountBullet[weaponConfiguration].AmountBullet <= 0 && isReload == false)
        {
            StartCoroutine(Reload(weaponConfiguration));
        }
    }

    private IEnumerator Reload(WeaponConfiguration weaponConfiguration)
    {
        if (amountBullet[weaponConfiguration].MaxBullet == 0 && SaveData.Instance.isInfinitBullet == false) yield break;
        isReload = true;
        animatorController.SetPlay(CharacterAnimationType.Reaload, true, 1);
        yield return new WaitForSeconds(2f);
        var currentBullet = weaponConfiguration.AmounBullet;
        if (SaveData.Instance.isInfinitBullet == false)
        {
            if (amountBullet[weaponConfiguration].MaxBullet - currentBullet < 0)
            {
                currentBullet = amountBullet[weaponConfiguration].MaxBullet;
            }

            amountBullet[weaponConfiguration].AmountBullet = currentBullet;
            if (SaveData.Instance.isInfinitBullet == false)
            {
                amountBullet[weaponConfiguration].MaxBullet -= currentBullet;
            }
        }
        else
        {
            amountBullet[weaponConfiguration].AmountBullet = weaponConfiguration.AmounBullet;
        }

        isReload = false;
    }

    private void Shoot()
    {
        if (_weaponEffectSpawner == null || _weaponEffectSpawner.gameObject.activeInHierarchy == false)
        {
            _weaponEffectSpawner = GetComponentInChildren<WeaponEffectSpawner>();
        }

        animatorController.SetPlay(CharacterAnimationType.Shot, false, 2);
        bulletSpawner.SpawnBullet(weaponConfiguration.BulletType, GetFixParameters(ParametersType.Damage),
            _weaponEffectSpawner.StartPositionEffect, targetSearcher.FoundedTarget.TargetPosition);
        Shoted?.Invoke();
    }

    private void Tick()
    {
        var first = isCanBeShot;
        isCanBeShot = targetSearcher.IsTargetFounded;
        if (first != isCanBeShot) shotCooldown = GetFixParameters(ParametersType.FireRate);
    }

    private void GenerateParametersWithParameters()
    {
        var saveData = SaveData.Instance.AttachList.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon)
            .ToList()[0];
        var damageBonus = 0f;
        var fireRateBonus = 0f;
        var critChansBonus = 0f;
        foreach (var attachType in saveData.AttachTypes)
        {
            var attachmentConfiguration = attachmentConfigurationConstructor.AllAttachments
                .Where(t => t.AttachType == attachType).ToList()[0];
            foreach (var attachConfig in attachmentConfiguration.AttachParameters)
            {
                switch (attachConfig.ParametersType)
                {
                    case ParametersType.Damage:
                        damageBonus += attachConfig.AddPercent;
                        break;
                    case ParametersType.FireRate:
                        fireRateBonus += attachConfig.AddPercent;
                        break;
                    case ParametersType.CritChance:
                        critChansBonus += attachConfig.AddPercent;
                        break;
                }
            }
        }

        var damage = weaponConfiguration.Damage +
                     (weaponConfiguration.Damage * damageBonus);

        var fireRate = weaponConfiguration.FireRate -
                       (weaponConfiguration.FireRate * fireRateBonus);

        var crit = weaponConfiguration.CritChans +
                   (weaponConfiguration.CritChans * critChansBonus);

        fixValue = new Dictionary<ParametersType, float>();
        fixValue.Add(ParametersType.Damage, damage);
        fixValue.Add(ParametersType.FireRate, fireRate);
        fixValue.Add(ParametersType.CritChance, crit);
    }

    private float GetFixParameters(ParametersType parametersType)
    {
        switch (parametersType)
        {
            case ParametersType.Damage:
                return fixValue[ParametersType.Damage];
                break;
            case ParametersType.FireRate:
                return fixValue[ParametersType.FireRate];
                break;
            case ParametersType.CritChance:
                return fixValue[ParametersType.CritChance];
                break;
        }

        return 0;
    }
}

public interface IWeaponMain
{
    WeaponConfiguration WeaponConfiguration { get; }
    float CritChance { get; }
}

public class BulletStorage
{
    public int AmountBullet;
    public int MaxBullet;
}