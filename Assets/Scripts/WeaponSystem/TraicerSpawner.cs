using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TraicerSpawner : MonoBehaviour, ITraicerSpawner
{
    [SerializeField] private TraicerConfiguration[] traicerConfigurations;

    private Dictionary<TraicerType, IPool<MonoPooled>> traicerPool;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<ITraicerSpawner>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<ITraicerSpawner>();
    }

    private void InitializePool()
    {
        traicerPool = new Dictionary<TraicerType, IPool<MonoPooled>>();
        foreach (var traicerConfiguration in traicerConfigurations)
        {
            var factory = new FactoryMonoObject<MonoPooled>(traicerConfiguration.TraicerPool.gameObject, transform);
            traicerPool.Add(traicerConfiguration.TraicerType, new Pool<MonoPooled>(factory, 1));
        }
    }

    public void SpawnTraicer(TraicerType traicerType, IDamageTaker damageTaker, float damage, Transform startPos,
        Vector3 endPos)
    {
        if (traicerPool == null)
        {
            InitializePool();
        }

        var newTraicer = traicerPool[traicerType].Pull();
        newTraicer.transform.position = startPos.position;
        newTraicer.transform.rotation = startPos.rotation;
        var endPosFix = endPos;
        endPosFix.y = startPos.position.y;
        newTraicer.transform.DOMove(endPosFix, 0.1f).OnComplete(() =>
        {
            damageTaker.TakeDamage(damage);
            newTraicer.ReturnToPool();
        });
    }
}