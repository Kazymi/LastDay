using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create ZombieSpawnConfiguration", fileName = "ZombieSpawnConfiguration",
    order = 0)]
public class ZombieSpawnConfiguration : ScriptableObject
{
    
    [field: SerializeField] public ZombieLevelConfiguration[] LevelConfigurations { get; private set; }
    [field: SerializeField] public float spawnInterval { get; private set; }
}