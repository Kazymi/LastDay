using EventBusSystem;

public interface ILocalizationUpdated : IGlobalSubscriber
{
    void UpdateLocalization();
}