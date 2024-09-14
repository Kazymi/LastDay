using System;
using UnityEngine;

[Serializable]
public class Localization
{
    [field: SerializeField] public string LocationID { get; private set; }
    [field: SerializeField] public LocalizationConfiguration[] LocalizationConfigurations { get; private set; }
}