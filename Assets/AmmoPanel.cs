using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanel : MonoBehaviour
{
    [SerializeField] private WeaponMain WeaponMain;
    [SerializeField] private TMP_Text maxBullet;
    [SerializeField] private TMP_Text amountBullet;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image slider;
    [SerializeField] private GameObject noAmmo;

    private void Update()
    {
        UpdateImages();
    }

    private void UpdateImages()
    {
        if (WeaponMain.WeaponConfiguration == null) return;
        if (SaveData.Instance.isInfinitBullet == false)
        {
            noAmmo.SetActive(WeaponMain.amountBullet[WeaponMain.WeaponConfiguration].AmountBullet == 0 &&
                             WeaponMain.amountBullet[WeaponMain.WeaponConfiguration].MaxBullet == 0);
        }
        else
        {
            noAmmo.gameObject.SetActive(false);
        }

        maxBullet.text = SaveData.Instance.isInfinitBullet
            ? Localizator.Instance.GetLocalization("infi")
            : WeaponMain.amountBullet[WeaponMain.WeaponConfiguration].MaxBullet.ToString();
        amountBullet.text = WeaponMain.amountBullet[WeaponMain.WeaponConfiguration].AmountBullet.ToString();
        weaponImage.sprite = WeaponMain.WeaponConfiguration.WeaponSpriteForGame;
        slider.fillAmount = (float) WeaponMain.amountBullet[WeaponMain.WeaponConfiguration].AmountBullet /
                            (float) WeaponMain.WeaponConfiguration.AmounBullet;
    }
}