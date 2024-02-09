using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    public static Collider GetNearest(List<Collider> allTargets, Vector3 searchParent)
    {
        float distance = Mathf.Infinity;
        Collider closest = null;
        Vector3 position = searchParent;
        foreach (Collider go in allTargets)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}