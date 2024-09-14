using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponSelectMenu : MonoBehaviour, IWeaponInGameChanged
{
    [SerializeField] private WeaponStorage weaponStorage;
    [SerializeField] private WeaponMain weaponMain;
    [SerializeField] private WeaponContainer weaponContainer;
    [SerializeField] private WeaponVersion weaponVersion;
    [SerializeField] private Image weaponImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selectImage;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => weaponContainer.ShowWeapon(weaponVersion));
        UpdateSelectedWeapon();
    }

    public void UpdateSelectedWeapon()
    {
        StartCoroutine(UpdateVisible());
    }

    private IEnumerator UpdateVisible()
    {
        yield return new WaitForSeconds(0.2f);
        var weaponType = SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == weaponVersion).ToList()[0]
            .WeaponType;
        if (weaponType == WeaponType.Clear)
        {
            lockImage.gameObject.SetActive(true);
            selectImage.gameObject.SetActive(false);
            yield break;
        }

        if (weaponType == WeaponType.MAuto || weaponType == WeaponType.MAutoSecond ||
            weaponType == WeaponType.MAutoThird)
        {
            if (SaveData.Instance.IsHeaveWeaponUnlock == false)
            {
                lockImage.gameObject.SetActive(true);
                selectImage.gameObject.SetActive(false);
                yield break;
            }
        }

        weaponImage.sprite =
            weaponStorage.WeaponClassificators.Where(t =>
                t.WeaponType == SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == weaponVersion).ToList()[0]
                    .WeaponType).ToList()[0].WeaponConfiguration.WeaponSpriteForGame;
        lockImage.gameObject.SetActive(false);
        var currentWeapon = weaponStorage.WeaponClassificators
            .Where(t => t.WeaponConfiguration == weaponMain.WeaponConfiguration).ToList()[0].WeaponType;
        selectImage.gameObject.SetActive(weaponType == currentWeapon);
    }
}

public interface IWeaponInGameChanged : IGlobalSubscriber
{
    void UpdateSelectedWeapon();
}