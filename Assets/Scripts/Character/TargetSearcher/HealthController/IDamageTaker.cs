using UnityEngine;

public interface IDamageTaker
{
    void TakeDamage(float damage);
    Vector3 takerPosition { get; }
}