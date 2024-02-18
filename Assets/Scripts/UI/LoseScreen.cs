using CrazyGames;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject loseScren;
    [SerializeField] private PlayerHealthBase playerHealthBase;
    [SerializeField] private TMP_Text earnedMoney;

    [SerializeField] private Button menuButton;
    [SerializeField] private Button reward;

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
    }

    private void OnDisable()
    {
        playerHealthBase.HealthEmpty -= LoseScreenShow;
        reward.onClick.RemoveListener(Reward);
        menuButton.onClick.RemoveListener(Claim);
    }

    private void LoseScreenShow()
    {
        earned = SaveData.Instance.Wallet.Money - startMoney;
        earnedMoney.text = earned.ToString();
        loseScren.gameObject.SetActive(true);
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
    }

    private void Claim()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene(0);
    }
}