using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class InvinsibleButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        StartCoroutine(Initialize());
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private const int RewardID = 3;

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
            SaveData.Instance.IsInvinsible = true;
            SaveData.Instance.Save();
            Destroy(gameObject);
        }
    }


    private void OnClick()
    {
        YandexGame.RewVideoShow(RewardID);
    }

    private IEnumerator Initialize()
    {
        yield return null;
        if (SaveData.Instance.IsInvinsible) Destroy(gameObject);
    }
}