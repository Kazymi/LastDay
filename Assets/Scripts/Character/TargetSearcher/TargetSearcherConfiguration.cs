using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create TargetSearcherConfiguration",
    fileName = "TargetSearcherConfiguration",
    order = 0)]
public class TargetSearcherConfiguration : ScriptableObject
{
    [field: SerializeField] public float SearchRadius { get; private set; }
    [field: SerializeField] public float SearchCooldown { get; private set; }
    [field: SerializeField] public LayerMask SearchMask { get; private set; }
    [field: SerializeField] public TargetType SearchType { get; private set; }
}