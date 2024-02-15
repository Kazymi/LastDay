namespace EventBusSystem
{
    public interface IGlobalSubscriber
    { }

    public interface IAttachmentUpdate : IGlobalSubscriber
    {
        void AttachmentUpdated();
    }
}