using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponScreen : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text damage;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private TMP_Text bullet;

    public void SetUp(WeaponConfiguration weaponConfiguration)
    {
        weaponImage.sprite = weaponConfiguration.WeaponSprite;
        name.text = weaponConfiguration.WeaponName;
        damage.text = (Convert.ToInt32(weaponConfiguration.Damage * 10)).ToString();
        speed.text = weaponConfiguration.FireRate.ToString();
        bullet.text = weaponConfiguration.AmounBullet.ToString();
    }
}