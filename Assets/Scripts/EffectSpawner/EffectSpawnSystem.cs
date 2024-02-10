using System;
using UnityEngine;

public enum EffectType
{
    ARShootEffect,
    Blood,
}

[Serializable]
public class EffectConfiguration
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private TemporaryMonoPooled effect;

    public EffectType EffectType => effectType;

    public TemporaryMonoPooled Effect => effect;
}