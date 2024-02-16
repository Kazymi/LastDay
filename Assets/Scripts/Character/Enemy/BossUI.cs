using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour, IBossUI
{
    [SerializeField] private AudioClip defaultClip;
    [SerializeField] private AudioClip bossClip;

    [SerializeField] private GameObject sliderPanel;
    [SerializeField] private TMP_Text bossText;
    [SerializeField] private Image bossSlider;

    private HealthController healthController;
    private ISoundSystem soundSystem;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IBossUI>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IBossUI>();
    }

    public void Initialize(HealthController healthController)
    {
        this.healthController = healthController;
        StartCoroutine(HealthbarController());
    }

    private IEnumerator HealthbarController()
    {
        soundSystem ??= ServiceLocator.GetService<ISoundSystem>();
        var config = soundSystem.GetSound(SoundType.Embient);
        config.ReplaceSound(bossClip);


        sliderPanel.gameObject.SetActive(true);
        bossSlider.fillAmount = 0;
        var textBoss = bossText.text;
        bossText.text = "";
        bossSlider.DOFillAmount(1, 1f);
        foreach (var text in textBoss)
        {
            bossText.text += text;
            yield return new WaitForSeconds(0.5f);
        }

        while (healthController.isDead == false)
        {
            bossSlider.fillAmount = healthController.currentDamagePerce;
            yield return null;
        }

        config.ReplaceSound(defaultClip);
        sliderPanel.gameObject.SetActive(false);
    }
}