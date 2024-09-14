using System.Collections.Generic;
using UnityEngine;

public class Localizator : MonoBehaviour
{
    [SerializeField] private LocalizationLibrary localizationLibrary;

    public static Localizator Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public string GetLocalization(string key)
    {
        foreach (var localization in localizationLibrary.Localizations)
        {
            if (localization.LocationID == key)
            {
                foreach (var localizationConfiguration in localization.LocalizationConfigurations)
                {
                    if (SaveData.Instance.LocalizationType == localizationConfiguration.LocalizationType)
                    {
                        return localizationConfiguration.Text;
                    }
                }
            }
        }

        return "Localization error";
    }
}