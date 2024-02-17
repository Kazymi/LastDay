using System;
using UnityEngine;

[Serializable]
public class QuestUIAnimationConfiguration
{
    [field: SerializeField] public Transform StartPosition { get; private set; }
    [field: SerializeField] public Transform EndPosition { get; private set; }
    [field: SerializeField] public Vector3 StartScale { get; private set; }
}