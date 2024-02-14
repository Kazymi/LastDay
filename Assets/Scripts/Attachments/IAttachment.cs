public interface IAttachment
{
    void ConnectAttachment(AttachmentList attachmentList, AttachType attachType);
    WeaponType CurrentWeapon { get; }
    void EnableOnlyCurrentList(AttachmentList attachmentList);
    void EnableAllList();
    AttachmentConfigurationConstructor AttachmentConfigurationConstructor { get; }
}