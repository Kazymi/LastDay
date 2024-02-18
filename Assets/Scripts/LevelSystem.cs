using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour, ILevelSystem
{
    [SerializeField] private bool isLoop = true;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int prepareTime;
    [SerializeField] private LevelSystemUI levelSystemUi;

    public int AmountZombie;

    private int startMoney;
    public int DeadZombie { get; set; } = -1;

    private IZombieSpawner zombieSpawner;

    private void Start()
    {
        SaveData.Instance.CurrentLevel = 0;
        zombieSpawner = ServiceLocator.GetService<IZombieSpawner>();
        Initialize();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<ILevelSystem>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<ILevelSystem>();
    }

    private void Update()
    {
        CheckLevel();
        if (DeadZombie >= AmountZombie)
        {
            if (isLoop)
            {
                SaveData.Instance.CurrentLevel++;
                DeadZombie = -1;
                Initialize();
            }
        }
    }

    private void Initialize()
    {
        StartCoroutine(InitializeTimer());
    }

    private void CheckLevel()
    {
        levelSystemUi.slider.fillAmount = 1f - ((float) DeadZombie / (float) AmountZombie);
    }

    private IEnumerator InitializeTimer()
    {
        levelSystemUi.sliderObject.gameObject.SetActive(false);
        levelSystemUi.readyText.gameObject.SetActive(true);
        if (prepareTime != 0)
        {
            var currentTime = prepareTime;
            while (currentTime > 0)
            {
                currentTime -= 1;
                levelSystemUi.readyText.SetText(currentTime.ToString());
                levelSystemUi.readyText.transform.localScale = Vector3.zero;
                levelSystemUi.readyText.transform.DOKill();
                levelSystemUi.readyText.transform.DOScale(Vector3.one, 1f);
                yield return new WaitForSeconds(1f);
            }
        }

        zombieSpawner.SpawnZombie();
        DeadZombie = 0;
        AmountZombie = zombieSpawner.CurrentZombieAmount;
        levelText.text = "Lvl" + (SaveData.Instance.CurrentLevel + 1).ToString();
        levelSystemUi.sliderObject.gameObject.SetActive(true);
        levelSystemUi.readyText.gameObject.SetActive(false);
    }
}

[Serializable]
public class LevelSystemUI
{
    [field: SerializeField] public Image slider { get; private set; }
    [field: SerializeField] public GameObject sliderObject { get; private set; }
    [field: SerializeField] public TMP_Text readyText { get; private set; }
}

public interface ILevelSystem
{
    int DeadZombie { set; get; }
}