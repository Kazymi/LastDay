using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour, IBossUI
{
    [SerializeField] private GameObject sliderPanel;
    [SerializeField] private TMP_Text bossText;
    [SerializeField] private Image bossSlider;

    private HealthController healthController;

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

        sliderPanel.gameObject.SetActive(false);
    }
}