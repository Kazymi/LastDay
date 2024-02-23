using System.Collections;
using CrazyGames;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    }

    private void OnDisable()
    {
        playerHealthBase.HealthEmpty -= LoseScreenShow;
        reward.onClick.RemoveListener(Reward);
        menuButton.onClick.RemoveListener(Claim);
        menu.onClick.RemoveListener(ToMenu);
    }

    private void ToMenu()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene(0);
    }

    private void LoseScreenShow()
    {
        earned = SaveData.Instance.Wallet.Money - startMoney;
        earnedMoney.text = earned.ToString();
        backImage.DOFade(0.9f, 0.7f).OnComplete(() =>
        {
            loseScreen.gameObject.SetActive(true);
            StartCoroutine(ScreenText("You're dead"));
        });

        buttonText.text = "Restart";
    }

    public void PlayNextStage()
    {
        earned = SaveData.Instance.Wallet.Money - startMoney;
        earnedMoney.text = earned.ToString();
        backImage.DOFade(0.9f, 0.7f).OnComplete(() =>
        {
            loseScreen.gameObject.SetActive(true);
            StartCoroutine(ScreenText("level completed"));
        });
        buttonText.text = "next level";
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
        CrazyAds.Instance.beginAdBreakRewarded(() =>
        {
            SaveData.Instance.Wallet.AddMoney(earned);
            earned *= 2;
            earnedMoney.text = earned.ToString();
        });
        menuButton.transform.position = centerButton.position;
    }

    private void Claim()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene(1);
    }
}