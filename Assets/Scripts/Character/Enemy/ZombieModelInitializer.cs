using UnityEngine;

public class ZombieModelInitializer : MonoBehaviour
{
    [SerializeField] private ZombieHealthController ZombieHealthController;
    [SerializeField] private GameObject[] zombieModel;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private GameObject[] backPacks;

    private GameObject weapon;
    private GameObject backPack;

    private void Awake()
    {
        ModelActivate();
    }

    private void OnEnable()
    {
        ZombieHealthController.HealthEmpty += ZombieDead;
    }

    private void OnDisable()
    {
        ZombieHealthController.HealthEmpty -= ZombieDead;
    }

    private void ZombieDead()
    {
        if (weapon != null)
        {
            weapon.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (backPack != null)
        {
            backPack.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void ModelActivate()
    {
        foreach (var zombieModel in zombieModel)
        {
            zombieModel.gameObject.SetActive(false);
        }

        zombieModel[Random.Range(0, zombieModel.Length)].gameObject.SetActive(true);
    }

    private void CustomInitalize()
    {
        var ifSpawnWeapon = Random.Range(0, 10) < 3;
        if (ifSpawnWeapon)
        {
            weapon = weapons[Random.Range(0, weapons.Length)];
            weapon.SetActive(true);
        }

        var ifSpawnFaces = Random.Range(0, 10) < 6;
        if (ifSpawnFaces)
        {
            var face = faces[Random.Range(0, faces.Length)];
            face.SetActive(true);
        }

        var ifSpawnBackPack = Random.Range(0, 10) < 3;
        if (ifSpawnBackPack)
        {
            backPack = backPacks[Random.Range(0, backPacks.Length)];
            backPack.gameObject.SetActive(true);
        }
    }
}