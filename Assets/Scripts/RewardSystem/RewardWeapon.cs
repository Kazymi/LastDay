using System.Collections;
using System.Linq;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardWeapon : MonoBehaviour
{
    [SerializeField] private WeaponVersion WeaponVersion;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private GameObject buyWeapon;
    [SerializeField] private Button rewardButton;

    private bool isRewarded = false;

    private void Awake()
    {
        StartCoroutine(WalkReload());
        rewardButton.onClick.AddListener(() =>
        {
            isRewarded = true;
            YandexGame.RewVideoShow(RewardID);
        });
    }

    private const int RewardID = 2;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Reward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Reward;
    }

    private void Reward(int id)
    {
        if (id == RewardID && isRewarded)
        {
            RewardCompleted();
        }
    }

    private IEnumerator WalkReload()
    {
        yield return null;
        Check();
    }

    private void Check()
    {
        if (SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion).ToList()[0].WeaponType ==
            WeaponType.Clear)
        {
            buyWeapon.gameObject.SetActive(false);
            rewardButton.gameObject.SetActive(true);
        }
        else
        {
            buyWeapon.gameObject.SetActive(true);
            rewardButton.gameObject.SetActive(false);
        }
    }

    private void RewardCompleted()
    {
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion).ToList()[0].WeaponType = _weaponType;
        SaveData.Instance.FreeWeapon = _weaponType;
        SaveData.Instance.CurrentWeaponVersion = WeaponVersion;
        EventBus.RaiseEvent<ISelectWeapon>(t => t.WeaponSelected());
        Check();
        SaveData.Instance.Save();
    }
}