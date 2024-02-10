using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSearcher : MonoBehaviour, ITargetSearcher
{
    [SerializeField] private TargetSearcherConfiguration targetSearcherConfiguration;

    private float currentTime;

    public bool IsTargetFounded => FoundedTarget != null;
    public ITargetable FoundedTarget { get; private set; }

    private void Update()
    {
        Tick();
        CheckCurrentTarget();
    }

    private void Tick()
    {
        if (currentTime <= 0)
        {
            currentTime = targetSearcherConfiguration.SearchCooldown;
            SearchTarget();
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void CheckCurrentTarget()
    {
        if (FoundedTarget != null)
        {
            if (FoundedTarget.IsActive == false)
            {
                FoundedTarget = null;
                return;
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

    private void SearchTarget()
    {
        var foundTarget = Physics.OverlapSphere(transform.position, targetSearcherConfiguration.SearchRadius,
            targetSearcherConfiguration.SearchMask);
        if (foundTarget.Length != 0)
        {
            var foundedTargetOfTargetType = foundTarget.Where(t =>
            {
                var iTargetable = t.GetComponent<ITargetable>();
                if (iTargetable != null && iTargetable.TargetType == targetSearcherConfiguration.SearchType)
                {
                    return true;
                }

                return false;
            }).ToList();
            if (foundedTargetOfTargetType.Count != 0)
            {
                var nearestITargetable = GetNearestTarget(foundedTargetOfTargetType);
                if (nearestITargetable != null)
                {
                    TargetFound(nearestITargetable);
                }
            }
        }
    }

    private ITargetable GetNearestTarget(List<Collider> allTargets)
    {
        var nearst = GameHelper.GetNearest(allTargets, transform.position);
        return nearst != null ? nearst.GetComponent<ITargetable>() : null;
    }

    protected virtual void TargetFound(ITargetable foundTarget)
    {
        FoundedTarget = foundTarget;
    }

    private void OnDrawGizmosSelected()
    {
        if (targetSearcherConfiguration == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetSearcherConfiguration.SearchRadius);
    }
}

public interface ITargetSearcher
{
    bool IsTargetFounded { get; }
    ITargetable FoundedTarget { get; }
}