using EventBusSystem;

public interface IJimSignal : IGlobalSubscriber
{
    void Finish();
}