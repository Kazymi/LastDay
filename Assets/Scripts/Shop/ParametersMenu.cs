using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParametersMenu : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject weaponContainer;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject updatePanel;

    [SerializeField] private WeaponStorage weaponStorage;
    [SerializeField] private Image damageImag;
    [SerializeField] private Image fireRate;
    [SerializeField] private Image amountImage;
    [SerializeField] private Image critChansImage;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        ShowParameters();
        selectButton.interactable = SaveData.Instance.FreeWeapon != SaveData.Instance.SelectedWeapon;
    }

    private void Awake()
    {
        selectButton.onClick.AddListener(Select);
        closeButton.onClick.AddListener(Close);
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
}