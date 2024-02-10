using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMain : MonoBehaviour
{
    [SerializeField] private WeaponConfiguration weaponConfiguration;

    private bool isCanBeShot;
    private float shotCooldown;

    private IPlayerTargetSearcher targetSearcher;
    private IPlayerAnimatorController animatorController;
    public event Action Shoted;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Tick();
        ShotCooldown();
        ReadyShotCheck();
    }

    private void ReadyShotCheck()
    {
        animatorController.SetBool(CharacterAnimationType.IsTargetInZone, isCanBeShot);
    }

    private void OnInit()
    {
        targetSearcher = ServiceLocator.GetService<IPlayerTargetSearcher>();
        animatorController = ServiceLocator.GetService<IPlayerAnimatorController>();
        animatorController.SetBool(CharacterAnimationType.Weapon, true);
    }

    private void ShotCooldown()
    {
        if (isCanBeShot == false)
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
        var damageTaker = targetSearcher.FoundedTarget.target.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(weaponConfiguration.Damage);

        Shoted?.Invoke();
    }

    private void Tick()
    {
        var first = isCanBeShot;
        isCanBeShot = targetSearcher.IsTargetFounded;
        if (first != isCanBeShot) shotCooldown = weaponConfiguration.FireRate;
    }
}