using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeDisabler : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.DOFade(0, 0.5f);
    }

    private void Start()
    {
        if (SaveData.Instance.IsTutorialLocationCompleted == false)
        {
            SceneManager.LoadScene(2);
        }
    }
}
