using UnityEditor.Build;
using UnityEngine;

public class SearchTarget : MonoBehaviour, ITargetable
{
    public bool IsActive => gameObject.activeInHierarchy && IsTargetAlive;
    public Transform target => transform;
    public Vector3 TargetPosition => transform.position;
    public bool IsTargetAlive = true;
    [field: SerializeField] public TargetType TargetType { get; private set; }
}