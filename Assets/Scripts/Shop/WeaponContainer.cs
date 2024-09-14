using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private bool isShop;
    [SerializeField] private WeaponSelector[] weaponSelectors;

    [SerializeField] private GameObject shop;

    private int currentId;
    private List<WeaponVersion> unlockWeapon;

    private void Awake()
    {
        StartCoroutine(WheelInit());
    }

    private IEnumerator WheelInit()
    {
        yield return null;
        unlockWeapon = new List<WeaponVersion>();
        unlockWeapon.Add(WeaponVersion.PP);
        unlockWeapon.Add(WeaponVersion.AR);
        unlockWeapon.Add(WeaponVersion.ShotGun);
        if (SaveData.Instance.IsHeaveWeaponUnlock) unlockWeapon.Add(WeaponVersion.Heavy);
        if (SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0].WeaponType !=
            WeaponType.Clear) unlockWeapon.Add(WeaponVersion.Bonus);
        if (SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond).ToList()[0]
                .WeaponType !=
            WeaponType.Clear) unlockWeapon.Add(WeaponVersion.BonusSecond);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentId = 0;
            ShowWeapon(WeaponVersion.PP);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentId = 1;
            ShowWeapon(WeaponVersion.AR);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentId = 2;
            ShowWeapon(WeaponVersion.ShotGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentId = 3;
            ShowWeapon(WeaponVersion.Heavy);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentId = 4;
            ShowWeapon(WeaponVersion.Bonus);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentId = 5;
            ShowWeapon(WeaponVersion.BonusSecond);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentId += 1;
            if (currentId > unlockWeapon.Count - 1) currentId = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentId -= 1;
            if (currentId < 0) currentId = unlockWeapon.Count - 1;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            ShowWeapon(unlockWeapon[currentId]);
        }
    }

    public void OpenShop()
    {
        gameObject.SetActive(false);
        shop.SetActive(true);
    }

    private void OnEnable()
    {
        if (buttonClose != null) buttonClose.gameObject.SetActive(true);
        StartCoroutine(PreInit());
    }

    private void OnDisable()
    {
        if (buttonClose != null)
            buttonClose.gameObject.SetActive(false);
    }

    private IEnumerator PreInit()
    {
        if (isShop == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        foreach (var weaponSelector in weaponSelectors)
        {
            weaponSelector.Weapon.gameObject.SetActive(weaponSelector.WeaponType ==
                                                       (isShop
                                                           ? SaveData.Instance.FreeWeapon
                                                           : SaveData.Instance.WeaponTypes
                                                               .Where(t => t.WeaponVersion == WeaponVersion.AR)
                                                               .ToList()[0].WeaponType));
        }
    }

    public void ShowWeapon(WeaponVersion weaponVersion)
    {
        if (weaponVersion == WeaponVersion.Heavy && SaveData.Instance.IsHeaveWeaponUnlock == false) return;
        var weaponType = SaveData.Instance.WeaponTypes
            .Where(t => t.WeaponVersion == weaponVersion)
            .ToList()[0].WeaponType;
        if (weaponType == WeaponType.Clear) return;
        foreach (var weapon in weaponSelectors)
        {
            weapon.Weapon.gameObject.SetActive(weapon.WeaponType == weaponType);
        }

        EventBus.RaiseEvent<IWeaponInGameChanged>(t => t.UpdateSelectedWeapon());
    }
}