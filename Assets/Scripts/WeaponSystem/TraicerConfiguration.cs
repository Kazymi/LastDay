using System;
using UnityEngine;

[Serializable]
public class TraicerConfiguration
{
    [field: SerializeField] public TraicerType TraicerType { get; private set; }
    [field: SerializeField] public MonoPooled TraicerPool { get; private set; }
}