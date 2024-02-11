using UnityEngine;

public interface IEffectSpawner
{
    void SpawnEffect(EffectType effectType, Transform position, bool customRotation = true);
}