using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DeadBloodEffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private HealthController ZombieHealthController;

    private void OnEnable()
    {
        ZombieHealthController.HealthEmpty += CharacterDead;
    }

    private void OnDisable()
    {
        ZombieHealthController.HealthEmpty -= CharacterDead;
    }

    private void CharacterDead()
    {
        StartCoroutine(SpawnEffect());
    }

    private IEnumerator SpawnEffect()
    {
        yield return new WaitForSeconds(1.5f);
        var prefabNew = Instantiate(prefab);
        prefabNew.transform.localScale = Vector3.zero;
        var spawnPosition = transform.position;
        if (Physics.Raycast(spawnPosition + (Vector3.up * 3), Vector3.down, out var hit))
        {
            if (hit.transform.tag == "Map")
            {
                prefabNew.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                prefabNew.transform.DOScale(0.5f, 5f);
            }
        }
    }
}