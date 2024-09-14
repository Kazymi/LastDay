using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelSystemUI
{
    [field: SerializeField] public Image slider { get; private set; }
    [field: SerializeField] public GameObject sliderObject { get; private set; }
    [field: SerializeField] public TMP_Text readyText { get; private set; }
}