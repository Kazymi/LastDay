using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private HealthController healthController;

    private void Start()
    {
        ServiceLocator.GetService<IBossUI>().Initialize(healthController);
    }
}