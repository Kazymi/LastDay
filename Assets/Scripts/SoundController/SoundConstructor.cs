using System;
using UnityEngine;

[Serializable]
public class SoundConstructor
{
    [SerializeField] private SoundType soundType;
    [SerializeField] private SoundConfiguration soundConfiguration;

    public SoundType SoundType => soundType;

    public SoundConfiguration SoundConfiguration => soundConfiguration;
}