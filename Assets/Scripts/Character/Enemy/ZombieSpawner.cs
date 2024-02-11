using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int spawnInterval;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private EnemyStateMachine[] casualZombie;

    private void Awake()
    {
        StartCoroutine(ZombieSpawn());
    }

    private IEnumerator ZombieSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCasualZombie();
        }
    }

    private void SpawnCasualZombie()
    {
        var pos = spawnPositions[Random.Range(0, spawnPositions.Length)];
        var zombie = casualZombie[Random.Range(0, casualZombie.Length)];
        var newZombie = Instantiate(zombie);
        newZombie.transform.position = pos.position;
    }
}