using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private Image[] image;

    private void Start()
    {
        ShowLogo();
        StartCoroutine(LoadGame());
    }

    private void ShowLogo()
    {
        foreach (var image in image)
        {
            //   image.DOFade(1, 1.4f).SetEase(ease).OnComplete(() => { image.DOFade(0, 1.5f); });
        }
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(1);
    }
}