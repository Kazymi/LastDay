using System;
using UnityEngine;

[Serializable]
public class WeaponClassificator
{
    [field: SerializeField] public WeaponConfiguration WeaponConfiguration { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
}