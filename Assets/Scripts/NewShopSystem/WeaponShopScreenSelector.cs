using System.Linq;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponScreen), typeof(Button))]
public class WeaponShopScreenSelector : MonoBehaviour, ISelectWeapon
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectColor;

    [SerializeField] private WeaponStorage weaponStorage;
    [SerializeField] private WeaponConfiguration _weaponConfiguration;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        WeaponSelected();
        GetComponent<WeaponScreen>().SetUp(_weaponConfiguration);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Select);
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        _button.onClick.RemoveListener(Select);
    }

    private void Select()
    {
        SaveData.Instance.FreeWeapon =
            weaponStorage.WeaponClassificators.Where(t => t.WeaponConfiguration == _weaponConfiguration).ToList()[0]
                .WeaponType;
        SaveData.Instance.CurrentWeaponVersion = _weaponConfiguration.WeaponVersion;
        EventBus.RaiseEvent<ISelectWeapon>(t => t.WeaponSelected());
    }

    public void WeaponSelected()
    {
        var weapon = weaponStorage.WeaponClassificators.Where(t => t.WeaponConfiguration == _weaponConfiguration)
            .ToList()[0]
            .WeaponType;
        _image.color = SaveData.Instance.FreeWeapon == weapon ? selectColor : defaultColor;
    }
}