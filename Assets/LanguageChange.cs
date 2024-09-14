using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LanguageChange : MonoBehaviour
{
    [SerializeField] private LocalizationType localizationType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SaveData.Instance.LocalizationType = localizationType;
        EventBus.RaiseEvent<ILocalizationUpdated>(t => t.UpdateLocalization());
        SaveData.Instance.Save();
    }
}