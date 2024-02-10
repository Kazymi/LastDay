using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create HealthConfiguration", fileName = "HealthConfiguration",
    order = 0)]
public class HealthConfiguration : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; }
}