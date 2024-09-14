using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;

public class ShopCharacterWeaponContaier : MonoBehaviour, ISelectWeapon
{
    [SerializeField] private WeaponShopContainer[] weaponShopContainers;
    [SerializeField] private WeaponShopContainer PP;
    [SerializeField] private WeaponShopContainer AR;
    [SerializeField] private WeaponShopContainer ShotGun;
    [SerializeField] private WeaponShopContainer MAuto;
    [SerializeField] private WeaponShopContainer Bonus;
    [SerializeField] private WeaponShopContainer SecondBonus;
    [SerializeField] private WeaponShopContainer Hand;

    private void Awake()
    {
        StartCoroutine(Cooldown());
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.1f);
        SaveData.Instance.FreeWeapon =
            SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.AR).ToList()[0].WeaponType;
        WeaponSelected();
    }

    public void WeaponSelected()
    {
        var current = SaveData.Instance.FreeWeapon;
        var currentType = SaveData.Instance.CurrentWeaponVersion;

        foreach (var shopContainer in weaponShopContainers)
        {
            shopContainer.Disable();
        }

        Hand.Setup(current);
        PP.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.PP).ToList()[0].WeaponType);
        AR.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.AR).ToList()[0].WeaponType);
        ShotGun.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.ShotGun).ToList()[0]
            .WeaponType);
        MAuto.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Heavy).ToList()[0]
            .WeaponType);

        if (SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0].WeaponType !=
            WeaponType.Clear)
        {
            Bonus.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0]
                .WeaponType);
        }

        if (SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond).ToList()[0]
            .WeaponType != WeaponType.Clear)
        {
            SecondBonus.Setup(SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond)
                .ToList()[0].WeaponType);

            switch (currentType)
            {
                case WeaponVersion.AR:
                    AR.Disable();
                    break;
                case WeaponVersion.PP:
                    PP.Disable();
                    break;
                case WeaponVersion.ShotGun:
                    ShotGun.Disable();
                    break;
                case WeaponVersion.Heavy:
                    MAuto.Disable();
                    break;
                case WeaponVersion.Bonus:
                    Bonus.Disable();
                    break;
                case WeaponVersion.BonusSecond:
                    SecondBonus.Disable();
                    break;
            }
        }
    }
}