using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create WeaponStorage", fileName = "WeaponStorage",
    order = 0)]
public class WeaponStorage : ScriptableObject
{
    [field:SerializeField] public WeaponClassificator[] WeaponClassificators { get; private set; }
}