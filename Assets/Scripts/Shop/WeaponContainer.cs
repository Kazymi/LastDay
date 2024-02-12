using System.Collections;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private bool isShop;
    [SerializeField] private WeaponSelector[] weaponSelectors;

    private void OnEnable()
    {
        StartCoroutine(PreInit());
    }

    private IEnumerator PreInit()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var weaponSelector in weaponSelectors)
        {
            weaponSelector.Weapon.gameObject.SetActive(weaponSelector.WeaponType ==
                                                       (isShop
                                                           ? SaveData.Instance.FreeWeapon
                                                           : SaveData.Instance.SelectedWeapon));
        }
    }
}