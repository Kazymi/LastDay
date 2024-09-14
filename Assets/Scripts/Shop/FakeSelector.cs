using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FakeSelector : MonoBehaviour
{
    [SerializeField] private WeaponVersion weaponVersion;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    private void Select()
    {
        SaveData.Instance.FreeWeapon =
            SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == weaponVersion).ToList()[0].WeaponType;
        SaveData.Instance.CurrentWeaponVersion = weaponVersion;
        EventBus.RaiseEvent<ISelectWeapon>(t => t.WeaponSelected());
        SaveData.Instance.Save();
    }
}