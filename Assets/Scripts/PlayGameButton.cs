using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayGameButton : MonoBehaviour, ILocalizationUpdated
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image fadeImage;

    private Button _button;

    private void OnEnable()
    {
        StartCoroutine(Initialize());
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private IEnumerator Initialize()
    {
        yield return null;
        levelText.text = $"{Localizator.Instance.GetLocalization("level")} " + (SaveData.Instance.CurrentLevel + 1);
    }

    private void OnClick()
    {
        _button.interactable = false;
        fadeImage.DOFade(1, 0.5f).OnComplete(() => { SceneManager.LoadScene(2); });
    }

    public void UpdateLocalization()
    {
        StartCoroutine(Initialize());
    }
}