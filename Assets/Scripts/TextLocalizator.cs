using System.Collections;
using EventBusSystem;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextLocalizator : MonoBehaviour, ILocalizationUpdated
{
    [SerializeField] private string id;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        StartCoroutine(Initialize());
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private IEnumerator Initialize()
    {
        yield return null;
        text.text = Localizator.Instance.GetLocalization(id);
    }

    public void UpdateLocalization()
    {
        StartCoroutine(Initialize());
    }
}