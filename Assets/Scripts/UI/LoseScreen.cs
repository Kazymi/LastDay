using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Image backImage;
    [SerializeField] private PlayerHealthBase playerHealthBase;
    [SerializeField] private TMP_Text earnedMoney;

    [SerializeField] private Transform centerButton;

    [SerializeField] private Button menuButton;
    [SerializeField] private Button reward;
    [SerializeField] private Button menu;

    private int startMoney;
    private int earned;
    private bool isFinish;

    private void Start()
    {
        startMoney = SaveData.Instance.Wallet.Money;
    }

    private void OnEnable()
    {
        playerHealthBase.HealthEmpty += LoseScreenShow;
        reward.onClick.AddListener(Reward);
        menuButton.onClick.AddListener(Claim);
        menu.onClick.AddListener(ToMenu);
        YandexGame.RewardVideoEvent += Reward;
    }

    private void OnDisable()
    {
        playerHealthBase.HealthEmpty -= LoseScreenShow;
        reward.onClick.RemoveListener(Reward);
        menuButton.onClick.RemoveListener(Claim);
        menu.onClick.RemoveListener(ToMenu);
        YandexGame.RewardVideoEvent -= Reward;
    }

    private const int RewardID = 4;

    private void Reward(int id)
    {
        if (id == RewardID)
        {
            SaveData.Instance.Wallet.AddMoney(earned);
            earned *= 2;
            earnedMoney.text = earned.ToString();
        }
    }


    private void ToMenu()
    {
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.IsInvinsible = false;
        SaveData.Instance.Save();
        YandexGame.FullscreenShow();
        SceneManager.LoadScene(1);
    }

    private void LoseScreenShow()
    {
        if (isFinish) return;
        isFinish = true;
        earned = SaveData.Instance.Wallet.Money - startMoney;
        earnedMoney.text = earned.ToString();
        reward.gameObject.SetActive(earned > 10);
        backImage.DOFade(0.9f, 0.7f).OnComplete(() =>
        {
            loseScreen.gameObject.SetActive(true);
            StartCoroutine(ScreenText(Localizator.Instance.GetLocalization("dead")));
        });

        buttonText.text = Localizator.Instance.GetLocalization("restart");
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.IsInvinsible = false;
    }

    public void PlayNextStage()
    {
        if (isFinish) return;
        isFinish = true;
        earned = SaveData.Instance.Wallet.Money - startMoney;
        earnedMoney.text = earned.ToString();
        backImage.DOFade(0.9f, 0.7f).OnComplete(() =>
        {
            loseScreen.gameObject.SetActive(true);
            StartCoroutine(ScreenText(Localizator.Instance.GetLocalization("win")));
        });
        buttonText.text = Localizator.Instance.GetLocalization("nlevel");
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.Bonus).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.WeaponTypes.Where(t => t.WeaponVersion == WeaponVersion.BonusSecond).ToList()[0].WeaponType =
            WeaponType.Clear;
        SaveData.Instance.IsInvinsible = false;
    }

    private IEnumerator ScreenText(string text)
    {
        var updateText = text;
        levelText.text = "";
        foreach (var word in updateText)
        {
            levelText.text += word;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Reward()
    {
        reward.interactable = false;
        reward.GetComponent<Animator>().enabled = false;
        reward.transform.DOScale(Vector3.one, 0.3f);
        reward.gameObject.SetActive(false);
        YandexGame.RewVideoShow(RewardID);
    }

    private void Claim()
    {
        YandexGame.FullscreenShow();
        SaveData.Instance.Save();
        SceneManager.LoadScene(2);
    }
}