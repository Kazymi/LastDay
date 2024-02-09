public class PlayerTargetSearcher : TargetSearcher, IPlayerTargetSearcher
{
    private void OnEnable()
    {
        ServiceLocator.Subscribe<IPlayerTargetSearcher>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IPlayerTargetSearcher>();
    }
}