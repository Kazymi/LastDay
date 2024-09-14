using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create WeaponConfiguration", fileName = "WeaponConfiguration",
    order = 0)]
public class WeaponConfiguration : ScriptableObject
{
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public int CritChans { get; private set; }
    [field: SerializeField] public int AmounBullet { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public WeaponVersion WeaponVersion { get; private set; }
    [field: SerializeField] public Sprite WeaponSprite { get; private set; }
    [field: SerializeField] public Sprite WeaponSpriteForGame { get; private set; }
    [field: SerializeField] public string WeaponName { get; private set; }
}

public enum WeaponVersion
{
    AR,
    PP,
    ShotGun,
    Heavy,
    Bonus,
    BonusSecond,
}