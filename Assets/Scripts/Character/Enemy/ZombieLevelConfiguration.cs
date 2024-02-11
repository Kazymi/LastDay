using System;
using UnityEngine;

[Serializable]
public class ZombieLevelConfiguration
{
    [field: SerializeField] public EnemyStateMachine[] enemyPrefabs { get; private set; }
    [field: SerializeField] public int amountEnemy { get; private set; }
    public int SpawnedZombie { get; set; }
}