using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSystem : MonoBehaviour, ILevelSystem
{
    [SerializeField] private ZombieSpawner[] zombieSpawner;
    [SerializeField] private ZombieSpawner havySpaner;
    [SerializeField] private LoseScreen loseScreen;
    [SerializeField] private bool isLoop = true;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int prepareTime;
    [SerializeField] private LevelSystemUI levelSystemUi;

    public int AmountZombie;

    private ZombieSpawner lastSpawner;
    private int startMoney;
    public int DeadZombie { get; set; } = -1;

    private void Start()
    {
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
                Debug.Log(SaveData.Instance.CurrentLevel);
                SaveData.Instance.Save();
                DeadZombie = -1;
                loseScreen.PlayNextStage();
                levelSystemUi.sliderObject.gameObject.SetActive(false);
                levelSystemUi.readyText.gameObject.SetActive(false);
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
        var zSpawner = SaveData.Instance.CurrentLevel <= 6
            ? zombieSpawner[Random.Range(0, zombieSpawner.Length)]
            : havySpaner;
        zSpawner.gameObject.SetActive(true);
        if (lastSpawner != null)
        {
            lastSpawner.gameObject.SetActive(false);
        }

        lastSpawner = zSpawner;
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

        zSpawner.SpawnZombie();
        DeadZombie = 0;
        AmountZombie = zSpawner.CurrentZombieAmount;
        levelText.text = $"{Localizator.Instance.GetLocalization("level")} " +
                         (SaveData.Instance.CurrentLevel + 1).ToString();
        levelSystemUi.sliderObject.gameObject.SetActive(true);
        levelSystemUi.readyText.gameObject.SetActive(false);
    }
}