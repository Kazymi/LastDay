using UnityEngine;
[CreateAssetMenu(menuName = "Configurations/Create LocalizationLibrary", fileName = "LocalizationLibrary",
    order = 0)]
public class LocalizationLibrary : ScriptableObject
{
    [field: SerializeField] public Localization[] Localizations { get; private set; }
}