using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfiguration weaponConfiguration;
    [SerializeField] private WeaponMain weaponMain;

    private void OnEnable()
    {
        weaponMain.Initialize(weaponConfiguration);
    }
}
