using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private GameObject upgradeObject;
    [SerializeField] private GameObject lockImageshake;
    [SerializeField] private ParametersMenu parametersMenu;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private WeaponListOne[] weapon;
    [SerializeField] private Transform spawnCenter;

    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selectButton;

    [SerializeField] private Button back;
    [SerializeField] private Button next;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button buyWeaponButton;

    private int currentLevel = 0;

    private GameObject currentWeapon;

    private void Awake()
    {
        back.onClick.AddListener(Back);
        buyWeaponButton.onClick.AddListener(BuyWeapon);
        next.onClick.AddListener(Next);
        upgradeButton.onClick.AddListener(OpenUpgrade);
        Open();
    }

    private void Open()
    {
        currentLevel = 0;
        UpdateParameters();
    }

    private void OpenUpgrade()
    {
        upgradeObject.gameObject.SetActive(true);
    }
    
    private void UpdateUI()
    {
        back.interactable = currentLevel != 0;
        next.interactable = currentLevel != weapon.Length - 1;
        var isAlreadyBuy = SaveData.Instance.BuyWeapon.Contains(weapon[currentLevel].WeaponType);
        selectButton.SetActive(isAlreadyBuy);
        upgradeButton.gameObject.SetActive(isAlreadyBuy);
        lockImage.SetActive(!isAlreadyBuy);
        weaponName.SetText(weapon[currentLevel].WeaponName);
        if (isAlreadyBuy == false)
        {
            lockImageshake.transform.DOShakeScale(0.2f, 0.3f)
                .OnComplete(() => lockImageshake.transform.DOScale(Vector3.one, 0));
            buyPanel.gameObject.SetActive(true);
            priceText.text = weapon[currentLevel].Price.ToString();
        }
        else
        {
            buyPanel.gameObject.SetActive(false);
        }

        buyWeaponButton.interactable = SaveData.Instance.Wallet.IsCanBeReduce(weapon[currentLevel].Price);
    }

    private void BuyWeapon()
    {
        SaveData.Instance.Wallet.ReduceMoney(weapon[currentLevel].Price);
        SaveData.Instance.BuyWeapon.Add(weapon[currentLevel].WeaponType);
        SaveData.Instance.Save();
        UpdateParameters();
    }

    private void UpdateModel()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }

        var currentModel = Instantiate(weapon[currentLevel].Prefab, spawnCenter);
        currentModel.transform.DOShakeScale(0.2f, 0.3f);
        currentWeapon = currentModel;
    }

    private void Next()
    {
        currentLevel++;
        UpdateParameters();
    }

    private void Back()
    {
        currentLevel--;
        UpdateParameters();
    }

    private void UpdateParameters()
    {
        UpdateModel();
        UpdateUI();
        UpdateSaveData();
    }

    private void UpdateSaveData()
    {
        SaveData.Instance.FreeWeapon = weapon[currentLevel].WeaponType;
        parametersMenu.UpdateVisible();
    }
}

[Serializable]
public class WeaponListOne
{
    [SerializeField] private int price;
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private GameObject prefab;

    public int Price => price;

    public string WeaponName => weaponName;

    public WeaponType WeaponType => weaponType;

    public GameObject Prefab => prefab;
}