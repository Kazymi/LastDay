using System.Collections;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private bool isShop;
    [SerializeField] private WeaponSelector[] weaponSelectors;

    [SerializeField] private GameObject shop;

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
                                                           : SaveData.Instance.SelectedWeapon));
        }
    }
}