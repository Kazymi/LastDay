using System;
using UnityEngine;

[Serializable]
public class SpriteConfiguration
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public SpriteType SpriteType { get; private set; }
}