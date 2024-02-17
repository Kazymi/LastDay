using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestUI
{
    [field: SerializeField] public TMP_Text Description { get; private set; }
    [field: SerializeField] public TMP_Text Progress { get; private set; }
    [field: SerializeField] public Image Image { get; private set; }
    [field: SerializeField] public Image Slider { get; private set; }
    [field: SerializeField] public Transform QuestPanel { get; private set; }
}