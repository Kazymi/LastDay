using EventBusSystem;

public interface ISelectWeapon : IGlobalSubscriber
{
    void WeaponSelected();
}