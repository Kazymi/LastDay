using UnityEngine;

public interface IDamagePopupSpawner
{
    void SpawnDamagePoput(Vector3 position, float damage, bool isCrit);
}