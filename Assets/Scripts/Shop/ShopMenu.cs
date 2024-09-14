using System.Linq;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour, IBuyObject, ISelectWeapon, IUpdateCurrentWeapon
{
    [SerializeField] private TMP_Text price;
    [SerializeField] private WeaponStorage weaponStorage;
    [SerializeField] private Button selectButton;
    [SerializeField] private GameObject selectObject;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button upgradeButton;

    private int currentPrice;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        selectButton.onClick.AddListener(Select);
        buyButton.onClick.AddListener(TryBuy);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        selectButton.onClick.RemoveListener(Select);
        buyButton.onClick.RemoveListener(TryBuy);
    }

    public void UpdateVisible()
    {
        if (SaveData.Instance.FreeWeapon == WeaponType.ARMinigun ||
            SaveData.Instance.FreeWeapon == WeaponType.ARReward)
        {
            selectButton.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            return;
        }

        var isBuy = SaveData.Instance.BuyWeapon.Contains(SaveData.Instance.FreeWeapon);
        if (isBuy)
        {
            selectButton.gameObject.SetActive(true);
            selectObject.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            selectButton.interactable =
                (SaveData.Instance.WeaponTypes.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon).ToList()
                    .Count == 0);
        }
        else
        {
            selectButton.gameObject.SetActive(false);
            selectObject.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            var foundPrice =
                weaponStorage.WeaponClassificators.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon).ToList()[0]
                    .WeaponConfiguration.Price;
            currentPrice = foundPrice;
            buyButton.interactable = SaveData.Instance.Wallet.IsCanBeReduce(foundPrice);
            price.text = foundPrice.ToString();
        }
    }

    private void Select()
    {
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == SaveData.Instance.CurrentWeaponVersion).ToList()[0]
            .WeaponType = SaveData.Instance.FreeWeapon;
        EventBus.RaiseEvent<IUpdateCurrentWeapon>(t => t.UpdateSelectedWeaponVisible());
        SaveData.Instance.Save();
    }

    private void TryBuy()
    {
        SaveData.Instance.Wallet.ReduceMoney(currentPrice);
        SaveData.Instance.BuyWeapon.Add(SaveData.Instance.FreeWeapon);
        EventBus.RaiseEvent<IBuyObject>(t => t.UpdateVisible());
    }

    public void WeaponSelected()
    {
        UpdateVisible();
    }

    public void UpdateSelectedWeaponVisible()
    {
        UpdateVisible();
    }
}