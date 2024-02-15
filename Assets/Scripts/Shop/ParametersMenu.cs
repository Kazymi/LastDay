using System;
using System.Linq;
using DG.Tweening;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParametersMenu : MonoBehaviour, IAttachmentUpdate
{
    [SerializeField] private AttachmentConfigurationConstructor attachmentConfigurationConstructor;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject weaponContainer;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject updatePanel;

    [SerializeField] private WeaponStorage weaponStorage;
    [SerializeField] private Image damageImag;
    [SerializeField] private Image damageBonusImag;
    [SerializeField] private Image fireRate;
    [SerializeField] private Image fireBonusRate;
    [SerializeField] private Image amountImage;
    [SerializeField] private Image amountBonusImage;
    [SerializeField] private Image critChansImage;
    [SerializeField] private Image critChansBonusImage;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        ShowParameters();
        EventBus.Subscribe(this);
        selectButton.interactable = SaveData.Instance.FreeWeapon != SaveData.Instance.SelectedWeapon;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Awake()
    {
        selectButton.onClick.AddListener(Select);
        closeButton.onClick.AddListener(Close);
    }

    private void UpdateBonusStats()
    {
        var saveData = SaveData.Instance.AttachList.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon)
            .ToList()[0];
        var damageBonus = 0f;
        var fireRateBonus = 0f;
        var critChansBonus = 0f;
        foreach (var attachType in saveData.AttachTypes)
        {
            var attachmentConfiguration = attachmentConfigurationConstructor.AllAttachments
                .Where(t => t.AttachType == attachType).ToList()[0];
            foreach (var attachConfig in attachmentConfiguration.AttachParameters)
            {
                switch (attachConfig.ParametersType)
                {
                    case ParametersType.Damage:
                        damageBonus += attachConfig.AddPercent;
                        break;
                    case ParametersType.FireRate:
                        fireRateBonus += attachConfig.AddPercent;
                        break;
                    case ParametersType.CritChance:
                        critChansBonus += attachConfig.AddPercent;
                        break;
                }
            }
        }

        var currentParameters =
            weaponStorage.WeaponClassificators.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon).ToList()[0];

        var damage = currentParameters.WeaponConfiguration.Damage +
                     (currentParameters.WeaponConfiguration.Damage * damageBonus);
        damageBonusImag.DOFillAmount(damage / 10f, 0.5f);

        var fireRate = currentParameters.WeaponConfiguration.FireRate -
                       (currentParameters.WeaponConfiguration.FireRate * fireRateBonus);
        fireBonusRate.DOFillAmount(1f - fireRate, 0.5f);

        var crit = currentParameters.WeaponConfiguration.CritChans +
                   (currentParameters.WeaponConfiguration.CritChans * critChansBonus);
        critChansBonusImage.DOFillAmount(crit / 15f, 0.5f);

        Debug.Log($"parameters with attach {damage} damage + {fireRate} fire {crit} crit");
    }


    private void ShowParameters()
    {
        fadeImage.DOFade(0.96f, 0.3f);
        weaponContainer.gameObject.SetActive(true);
        weaponContainer.transform.DOKill();
        weaponContainer.transform.localScale = Vector3.zero;
        weaponContainer.transform.DOScale(1, 0.5f);
        var currentParameters =
            weaponStorage.WeaponClassificators.Where(t => t.WeaponType == SaveData.Instance.FreeWeapon).ToList()[0];
        damageImag.DOFillAmount(currentParameters.WeaponConfiguration.Damage / 10f, 0.5f);
        fireRate.DOFillAmount(1f - currentParameters.WeaponConfiguration.FireRate, 0.5f);
        amountImage.DOFillAmount(
            (currentParameters.WeaponConfiguration.BulletType == BulletType.ShootGun ? 8f : 1f) / 10f, 0.5f);
        critChansImage.DOFillAmount(currentParameters.WeaponConfiguration.CritChans / 15f, 0.5f);
        UpdateBonusStats();
    }

    private void Close()
    {
        fadeImage.DOFade(0, 0.3f);
        weaponContainer.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(true);
        updatePanel.gameObject.SetActive(false);
    }

    private void Select()
    {
        SaveData.Instance.SelectedWeapon = SaveData.Instance.FreeWeapon;
        selectButton.interactable = SaveData.Instance.FreeWeapon != SaveData.Instance.SelectedWeapon;
        SaveData.Instance.Save();
    }

    public void AttachmentUpdated()
    {
        UpdateBonusStats();
    }
}