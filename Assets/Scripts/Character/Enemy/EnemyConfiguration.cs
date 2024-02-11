using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create EnemyConfiguration", fileName = "EnemyConfiguration",
    order = 0)]
public class EnemyConfiguration : ScriptableObject
{
    [field: SerializeField] public int AddMoney { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
}