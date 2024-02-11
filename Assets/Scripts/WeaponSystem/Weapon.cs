using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfiguration weaponConfiguration;
    [SerializeField] private WeaponMain weaponMain;

    private void Awake()
    {
        weaponMain.Initialize(weaponConfiguration);
    }
}
