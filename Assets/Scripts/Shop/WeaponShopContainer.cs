using System.Linq;
using UnityEngine;

public class WeaponShopContainer : MonoBehaviour
{
    [SerializeField] private WeaponSelector[] weaponSelectors;

    public void Setup(WeaponType weaponType)
    {
        weaponSelectors.Where(t => t.WeaponType == weaponType).ToList()[0].Weapon.gameObject.SetActive(true);
    }

    public void Disable()
    {
        foreach (var weaponSelector in weaponSelectors)
        {
            weaponSelector.Weapon.gameObject.SetActive(false);
        }
    }
}