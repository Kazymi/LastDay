using DG.Tweening;
using UnityEngine;

public class ZombieModelInitializer : MonoBehaviour
{
    [SerializeField] private Color deadColor;
    [SerializeField] private ZombieHealthController ZombieHealthController;
    [SerializeField] private GameObject[] zombieModel;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private GameObject[] backPacks;

    public SkinnedMeshRenderer meshRenderer { get; private set; }
    private MeshRenderer _meshRenderer;

    private GameObject weapon;
    private GameObject backPack;
    private bool isDead = false;

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
        if (isDead == false)
        {
            _meshRenderer.material.DOColor(deadColor, "_Color", 0.3f);
            meshRenderer.material.DOColor(deadColor, "_Color", 0.3f);
            isDead = true;
        }

        if (weapon != null)
        {
            weapon.transform.parent = null;
            weapon.GetComponent<Rigidbody>().isKinematic = false;
           Destroy(weapon,25);
        }

        if (backPack != null)
        {
            backPack.GetComponent<Rigidbody>().isKinematic = false;
            backPack.transform.parent = null;
            Destroy(backPack,25);
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

        if (faces.Length > 0)
        {
            var face = faces[Random.Range(0, faces.Length)];
            _meshRenderer = face.GetComponent<MeshRenderer>();
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