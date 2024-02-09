using UnityEngine;

public class SearchTarget : MonoBehaviour, ITargetable
{
    public bool IsActive => gameObject.activeInHierarchy;
    public Vector3 TargetPosition => transform.position;
    [field: SerializeField] public TargetType TargetType { get; private set; }
}