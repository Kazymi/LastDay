using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create WeaponConfiguration", fileName = "WeaponConfiguration",
    order = 0)]
public class WeaponConfiguration : ScriptableObject
{
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public int CritChans { get; private set; }
}