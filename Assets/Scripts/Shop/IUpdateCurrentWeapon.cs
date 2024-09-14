using EventBusSystem;

public interface IUpdateCurrentWeapon : IGlobalSubscriber
{
    void UpdateSelectedWeaponVisible();
}