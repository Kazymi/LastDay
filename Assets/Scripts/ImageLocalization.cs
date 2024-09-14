using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageLocalization : MonoBehaviour, ILocalizationUpdated
{
    private Image image;

    private void OnEnable()
    {
        StartCoroutine(Initialize());
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private IEnumerator Initialize()
    {
        yield return null;
        UpdateLocalization();
    }

    [SerializeField] private ImageLocalizationConfiguration[] imageLocalizationConfigurations;

    public void UpdateLocalization()
    {
        image ??= GetComponent<Image>();
        foreach (var imageLocalizationConfiguration in imageLocalizationConfigurations)
        {
            if (imageLocalizationConfiguration.LocalizationType == SaveData.Instance.LocalizationType)
            {
                image.sprite = imageLocalizationConfiguration.sprite;
            }
        }
    }
}

[Serializable]
public class ImageLocalizationConfiguration
{
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public LocalizationType LocalizationType { get; private set; }
}