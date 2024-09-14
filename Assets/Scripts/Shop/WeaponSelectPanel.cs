using System;
using System.Collections;
using System.Linq;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectPanel : MonoBehaviour, IUpdateCurrentWeapon
{
    private void Awake()
    {
        StartCoroutine(Cooldwon());
    }

    private void OnEnable()
    {
        StartCoroutine(Cooldwon());
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    [SerializeField] private TMP_Text damage;
    [SerializeField] private TMP_Text bulletamount;
    [SerializeField] private TMP_Text fireSpeed;
    [SerializeField] private TMP_Text name;
    [SerializeField] private Image weaponImage;

    [SerializeField] private WeaponStorage weaponContainer;
    [SerializeField] private WeaponVersion weaponVersion;

    private IEnumerator Cooldwon()
    {
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }

    public void Initialize()
    {
        var foundWeapon =
            weaponContainer.WeaponClassificators.Where(
                    t => t.WeaponType ==
                         SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == weaponVersion).ToList()[0]
                             .WeaponType)
                .ToList()[0].WeaponConfiguration;
        weaponImage.sprite = foundWeapon.WeaponSprite;
        name.text = foundWeapon.WeaponName;
        damage.text = (Convert.ToInt32(foundWeapon.Damage * 10)).ToString();
        fireSpeed.text = foundWeapon.FireRate.ToString();
        bulletamount.text = foundWeapon.AmounBullet.ToString();
    }

    public void UpdateSelectedWeaponVisible()
    {
        Initialize();
    }
}