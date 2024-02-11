using System.Collections;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour, IZombieSpawner
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private ZombieSpawnConfiguration[] casualZombie;

    public int CurrentZombieAmount { get; private set; }

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
            SpawnZombie(currentLvel);
        }
    }

    private void SpawnZombie(int levelKey)
    {
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
        newZombie.transform.position = pos.position;
    }
}

public interface IZombieSpawner
{
    int CurrentZombieAmount { get; }

    void SpawnZombie();
}