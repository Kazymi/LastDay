using System;
using System.Collections;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class LeaderBoard : MonoBehaviour, ILocalizationUpdated
{
    [SerializeField] private RawImage playerImage;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private LeaderBoardConfiguration[] _leaderBoardConfigurations;

    private void Start()
    {
        StartCoroutine(WaitForInitializeLeaderBoard());
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        YandexGame.onGetLeaderboard += OnGetLeaderboard;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        YandexGame.onGetLeaderboard -= OnGetLeaderboard;
    }

    private IEnumerator WaitForInitializeLeaderBoard()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        YandexGame.NewLeaderboardScores("DestroyerOfInfected", YandexGame.savesData.KilledZombie);
        YandexGame.GetLeaderboard("DestroyerOfInfected", 4, 4,
            4, "small");
        StartCoroutine(LoadImage(playerImage, YandexGame.playerPhoto));
        var playerName = YandexGame.playerName;
        if (playerName == "unauthorized")
        {
            playerName = Localizator.Instance.GetLocalization("auth");
        }

        this.playerName.text = playerName;
    }

    private void OnGetLeaderboard(LBData lb)
    {
        Debug.Log("Set");
        if (lb.technoName == "DestroyerOfInfected")
        {
            Debug.Log(lb.players.Length);
            for (int i = 0; i < lb.players.Length; i++)
            {
                if (_leaderBoardConfigurations.Length <= i) return;
                _leaderBoardConfigurations[i].LeaderBoard.SetActive(true);
                StartCoroutine(LoadImage(_leaderBoardConfigurations[i].Image, lb.players[i].photo));
                _leaderBoardConfigurations[i].Name.text = lb.players[i].name;
                _leaderBoardConfigurations[i].Description.text = lb.players[i].score.ToString();
            }
        }
    }

    private IEnumerator LoadImage(RawImage rawImage, string url)
    {
        WWW www = new WWW(url);
        yield return www;
        rawImage.texture = www.texture;
    }

    public void UpdateLocalization()
    {
        var playerName = YandexGame.playerName;
        if (playerName == "unauthorized")
        {
            playerName = Localizator.Instance.GetLocalization("auth");
        }

        this.playerName.text = playerName;
    }
}

[Serializable]
public class LeaderBoardConfiguration
{
    [SerializeField] private RawImage image;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject leaderBoard;

    public GameObject LeaderBoard => leaderBoard;

    public RawImage Image => image;

    public TMP_Text Name => name;

    public TMP_Text Description => description;
}