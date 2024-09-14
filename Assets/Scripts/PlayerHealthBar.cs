using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private PlayerHealthBase playerHealthBase;

    private bool isStart = true;
    private void OnEnable()
    {
        playerHealthBase.HealthUpdated += HealthUpdate;
    }

    private void OnDisable()
    {
        playerHealthBase.HealthUpdated -= HealthUpdate;
    }
    private void HealthUpdate()
    {
        if (isStart)
        {
            isStart = false;
            healthBar.DOFillAmount(playerHealthBase.currentDamagePerce, 0.2f).OnComplete(() => StartCoroutine(Fade()));
            return;
        }
        StopAllCoroutines();
        healthBar.DOKill();
        healthBar.DOFade(1, 0.2f);
        healthBar.DOFillAmount(playerHealthBase.currentDamagePerce, 0.2f).OnComplete(() => StartCoroutine(Fade()));
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        healthBar.DOFade(0, 0.4f);
    }
}