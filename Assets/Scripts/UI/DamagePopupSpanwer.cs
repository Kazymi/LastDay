using Unity.VisualScripting;
using UnityEngine;

public class DamagePopupSpanwer : MonoBehaviour, IDamagePopupSpawner
{
    [SerializeField] private Color critColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private DamagePopup prefab;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IDamagePopupSpawner>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IDamagePopupSpawner>();
    }

    public void SpawnDamagePoput(Vector3 position, float damage, bool isCrit)
    {
        var newPrefab = Instantiate(prefab);
        newPrefab.transform.position = position;
        newPrefab.Setup((int) damage, isCrit ? critColor : defaultColor,isCrit);
    }
}