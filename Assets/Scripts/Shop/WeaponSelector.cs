using System;
using UnityEngine;

[Serializable]
public class WeaponSelector
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public GameObject Weapon { get; private set; }
}