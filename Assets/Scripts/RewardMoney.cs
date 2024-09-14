using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RewardMoney : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private const int RewardID = 6;

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
        if (id == RewardID)
        {
            SaveData.Instance.Wallet.AddMoney(500);
            Destroy(gameObject);
        }
    }

    private void Click()
    {
        YandexGame.RewVideoShow(RewardID);
    }
}