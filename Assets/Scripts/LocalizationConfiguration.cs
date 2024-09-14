using System;
using UnityEngine;

[Serializable]
public class LocalizationConfiguration
{
    [field: SerializeField] public LocalizationType LocalizationType { get; private set; }
    [field: SerializeField] public string Text { get; private set; }
}