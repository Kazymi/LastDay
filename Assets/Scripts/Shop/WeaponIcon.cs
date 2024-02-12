using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WeaponIcon : MonoBehaviour
{
    [SerializeField] private GameObject paramentersMenu;
    [SerializeField] private GameObject shopMenu;

    [SerializeField] private WeaponType weaponType;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SaveData.Instance.FreeWeapon = weaponType;
        paramentersMenu.gameObject.SetActive(true);
        shopMenu.gameObject.SetActive(false);
    }
}