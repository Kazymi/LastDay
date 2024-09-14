using UnityEngine;

public class PlayerTargetSearcher : TargetSearcher, IPlayerTargetSearcher
{
    [SerializeField] private Transform head;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IPlayerTargetSearcher>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IPlayerTargetSearcher>();
    }

    protected override void CheckCurrentTarget()
    {
        if (FoundedTarget != null)
        {
            if (FoundedTarget.IsActive == false)
            {
                FoundedTarget = null;
                return;
            }

            if (head != null)
            {
                Vector3 targetDir = FoundedTarget.TargetPosition;
                Vector3 myPosition = head.position;
                if (Physics.Linecast(myPosition, targetDir, out var hit))
                {
                    if (hit.collider.GetComponent<SearchTarget>() == false)
                    {
                        FoundedTarget = null;
                        return;
                    }
                }
            }

            var distance = Vector3.Distance(FoundedTarget.TargetPosition, transform.position);
            var addSearchBehindPercent = 0.25f;
            if (distance > (targetSearcherConfiguration.SearchRadius +
                            (targetSearcherConfiguration.SearchRadius * addSearchBehindPercent)))
            {
                FoundedTarget = null;
            }
        }
    }
}