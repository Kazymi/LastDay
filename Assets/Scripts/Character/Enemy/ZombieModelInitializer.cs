using CrazyGames;
using UnityEngine;

public class ZombieModelInitializer : MonoBehaviour
{
    [SerializeField] private ZombieHealthController ZombieHealthController;
    [SerializeField] private GameObject[] zombieModel;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private GameObject[] backPacks;

    public SkinnedMeshRenderer meshRenderer { get; private set; }
    
    private GameObject weapon;
    private GameObject backPack;

    private void Awake()
    {
        ModelActivate();
        CustomInitalize();
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
            weapon.transform.parent = null;
            weapon.GetComponent<Rigidbody>().isKinematic = false;
            CrazySDK.Instance.GetSystemInfo(systemInfo =>
            {
                if (systemInfo.device.type == "desktop")
                {
                    Destroy(weapon,25f);
                }
                else
                {
                    Destroy(weapon);
                }
            });
        }

        if (backPack != null)
        {
            backPack.GetComponent<Rigidbody>().isKinematic = false;
            backPack.transform.parent = null;
            CrazySDK.Instance.GetSystemInfo(systemInfo =>
            {
                if (systemInfo.device.type == "desktop")
                {
                    Destroy(backPack,25f);
                }
                else
                {
                    Destroy(backPack);
                }
            });
        }
    }

    private void ModelActivate()
    {
        foreach (var zombieModel in zombieModel)
        {
            zombieModel.gameObject.SetActive(false);
        }

        var id = Random.Range(0, zombieModel.Length);
        zombieModel[id].gameObject.SetActive(true);
        meshRenderer = zombieModel[id].GetComponent<SkinnedMeshRenderer>();
    }

    private void CustomInitalize()
    {
        var ifSpawnWeapon = Random.Range(0, 10) < 3;
        if (ifSpawnWeapon && weapons.Length > 0)
        {
            weapon = weapons[Random.Range(0, weapons.Length)];
            weapon.SetActive(true);
        }

        var ifSpawnFaces = Random.Range(0, 10) < 6;
        if (ifSpawnFaces && faces.Length > 0)
        {
            var face = faces[Random.Range(0, faces.Length)];
            face.SetActive(true);
        }

        var ifSpawnBackPack = Random.Range(0, 10) < 3;
        if (ifSpawnBackPack && backPacks.Length > 0)
        {
            backPack = backPacks[Random.Range(0, backPacks.Length)];
            backPack.gameObject.SetActive(true);
        }
    }
}