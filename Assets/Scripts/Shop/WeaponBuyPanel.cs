using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WeaponBuyPanel : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int price;

    private void Start()
    {
        GetComponentInChildren<TMP_Text>().SetText(price.ToString());
        if (SaveData.Instance.BuyWeapon.Contains(weaponType))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (SaveData.Instance.Wallet.IsCanBeReduce(price))
        {
            SaveData.Instance.Wallet.ReduceMoney(price);
            SaveData.Instance.BuyWeapon.Add(weaponType);
            Destroy(gameObject);
        }
    }
}