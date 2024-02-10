using System;
using UnityEngine;

[Serializable]
public class HealthHitConfiguration
{
    [field: SerializeField] public EffectType HitEffect { get; private set; }
    [field: SerializeField] public Transform[] HitObject { get; private set; }
    [field: SerializeField] public Transform []HitPosition { get; private set; }
    [field: SerializeField] public float ZRotate { get; private set; }

    [field: Range(0f, 4f)]
    [field: SerializeField]
    public float DamageModificator { get; private set; }
}