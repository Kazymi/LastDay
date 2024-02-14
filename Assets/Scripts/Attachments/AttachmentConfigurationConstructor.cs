using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Weapon Attachments/Create AttachmentConfigurationConstructor", fileName = "AttachmentConfigurationConstructor",
    order = 0)]
public class AttachmentConfigurationConstructor : ScriptableObject
{
    [field: SerializeField] public AttachConfiguration[] AllAttachments { get; private set; }
}