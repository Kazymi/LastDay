using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RewardAmmo : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private const int RewardID = 5;

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
            SaveData.Instance.isInfinitBullet = true;
            Destroy(gameObject);
        }
    }

    private void Click()
    {
        YandexGame.RewVideoShow(RewardID);
    }
}