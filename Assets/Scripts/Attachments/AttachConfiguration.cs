using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Weapon Attachments/Create AttachConfiguration", fileName = "AttachConfiguration",
    order = 0)]
public class AttachConfiguration : ScriptableObject
{
    [field: SerializeField] public AttachType AttachType { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public GameObject attachObject { get; private set; }
}