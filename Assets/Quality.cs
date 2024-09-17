using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Quality : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(SetQuality());
    }

    private IEnumerator SetQuality()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        QualitySettings.SetQualityLevel(YandexGame.EnvironmentData.isMobile ? 0 : 1);
    }
}