using UnityEngine;

public interface ITargetable
{
    Vector3 TargetPosition { get; }
    TargetType TargetType { get; }
    bool IsActive { get; }
    Transform target { get; }
}