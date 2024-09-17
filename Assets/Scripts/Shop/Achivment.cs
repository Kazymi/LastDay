using TMPro;
using UnityEngine;
using YG;

public class Achivment : MonoBehaviour
{
    [SerializeField] private bool isZombieKill;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        if (isZombieKill)
        {
            text.text = YandexGame.savesData.KilledZombie.ToString();
        }
        else
        {
            text.text = SaveData.Instance.CurrentLevel.ToString();
        }
    }
}