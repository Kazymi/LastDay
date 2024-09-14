using UnityEngine;

public interface ITraicerSpawner
{
    void SpawnTraicer(TraicerType traicerType, IDamageTaker damageTaker, float damage, Transform startPos,
        Vector3 endPos);
}