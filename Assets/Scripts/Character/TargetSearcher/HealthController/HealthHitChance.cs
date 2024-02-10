using System;
using UnityEngine;

[Serializable]
public class HealthHitChance
{
    [field: Range(0, 15)]
    [field: SerializeField]
    public int Chance { get; private set; }

    public bool IsAnimated { get; set; }
    [field: SerializeField] public HealthHitConfiguration HealthHitConfiguration { get; private set; }
}