using System;
using System.Collections;
using System.Linq;
using CrazyGames;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour, IZombieSpawner
{
    [SerializeField] private bool isGizmos;
    [SerializeField] private ZombieSpawnPositionConfiguration[] spawnPositions;
    [SerializeField] private ZombieSpawnConfiguration[] casualZombie;

    public int CurrentZombieAmount { get; private set; }

    public bool AllZombieKilled = false;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IZombieSpawner>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IZombieSpawner>();
    }

    public void SpawnZombie()
    {
        SaveData.Instance.SpawnedZombie = 0;
        var currentLvel = SaveData.Instance.CurrentLevel;
        if (currentLvel >= casualZombie.Length) currentLvel = casualZombie.Length - 1;
        CurrentZombieAmount = 0;
        foreach (var configuration in casualZombie[currentLvel].LevelConfigurations)
        {
            CurrentZombieAmount += configuration.amountEnemy;
            configuration.SpawnedZombie = configuration.amountEnemy;
        }

        StartCoroutine(ZombieSpawn(currentLvel));
    }

    private IEnumerator ZombieSpawn(int currentLvel)
    {
        while (true)
        {
            yield return new WaitForSeconds(casualZombie[currentLvel].spawnInterval);
            var maxZombie = 60;
            CrazySDK.Instance.GetSystemInfo(systemInfo =>
            {
                if (systemInfo.device.type == "desktop")
                {
                }
                else
                {
                    maxZombie = 14;
                }
            });
            if (SaveData.Instance.SpawnedZombie >= maxZombie) continue;
            SpawnZombie(currentLvel);
        }
    }

    private void SpawnZombie(int levelKey)
    {
        SaveData.Instance.SpawnedZombie++;
        var pos = spawnPositions[Random.Range(0, spawnPositions.Length)];
        var zombie = casualZombie[levelKey];
        var zombieid = zombie.LevelConfigurations.Where(t => t.SpawnedZombie > 0).ToList();
        if (zombieid.Count == 0)
        {
            StopAllCoroutines();
            return;
        }

        var randomZombie = zombieid[Random.Range(0, zombieid.Count)];
        randomZombie.SpawnedZombie--;
        var newZombie = Instantiate(randomZombie.enemyPrefabs[Random.Range(0, randomZombie.enemyPrefabs.Length)]);
        var position = GetSpawnPosition();
        newZombie.transform.position = position;
    }

    private void OnDrawGizmos()
    {
        if (isGizmos == false) return;
        foreach (var targetSearcherConfiguration in spawnPositions)
        {
            if (targetSearcherConfiguration.SpawnCenter == null)
            {
                continue;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(targetSearcherConfiguration.SpawnCenter.position,
                new Vector3(targetSearcherConfiguration.RadiusX, 1, targetSearcherConfiguration.RadiusZ));
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var randomId = spawnPositions[Random.Range(0, spawnPositions.Length)];
        var position = randomId.SpawnCenter.position;
        var radiusXFix = randomId.RadiusX / 2f;
        var radiusZFix = randomId.RadiusZ / 2f;
        position += new Vector3(Random.Range(-radiusXFix, radiusXFix), 0, Random.Range(-radiusZFix, radiusZFix));
        return position;
    }
}

public interface IZombieSpawner
{
    int CurrentZombieAmount { get; }

    void SpawnZombie();
}

[Serializable]
public class ZombieSpawnPositionConfiguration
{
    [field: SerializeField] public Transform SpawnCenter { get; private set; }
    [field: SerializeField] public float RadiusX { get; private set; }
    [field: SerializeField] public float RadiusZ { get; private set; }
}