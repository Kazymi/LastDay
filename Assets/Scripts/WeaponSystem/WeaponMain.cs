using System;
using UnityEngine;

public class WeaponMain : MonoBehaviour, IWeaponMain
{
    [SerializeField] private PlayerHealthBase playerHealthBase;
    [SerializeField] private bool readyToShot;
    private WeaponConfiguration weaponConfiguration;

    private bool isCanBeShot;
    public bool CanBeShot => isCanBeShot && readyToShot;

    private float shotCooldown;

    private IBulletSpawner bulletSpawner;
    private IPlayerTargetSearcher targetSearcher;
    private IPlayerAnimatorController animatorController;
    public WeaponConfiguration WeaponConfiguration => weaponConfiguration;
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
    }

    public void Initialize(WeaponConfiguration weaponConfiguration)
    {
        this.weaponConfiguration = weaponConfiguration;
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

        if (shotCooldown <= 0)
        {
            Shoot();
            shotCooldown = weaponConfiguration.FireRate;
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        animatorController.SetPlay(CharacterAnimationType.Shot, false, 2);
        bulletSpawner.SpawnBullet(weaponConfiguration.BulletType, weaponConfiguration.Damage);
        Shoted?.Invoke();
    }

    private void Tick()
    {
        var first = isCanBeShot;
        isCanBeShot = targetSearcher.IsTargetFounded;
        if (first != isCanBeShot) shotCooldown = weaponConfiguration.FireRate;
    }
}

public interface IWeaponMain
{
    WeaponConfiguration WeaponConfiguration { get; }
}