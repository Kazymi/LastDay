using UnityEngine;

public class WeaponEffectSpawner : MonoBehaviour
{
    [SerializeField] private WeaponMain weaponMain;
    [SerializeField] private Transform startPositionEffect;
    [SerializeField] private EffectType effectType;

    private IEffectSpawner effectSpawner;

    private void Start()
    {
        effectSpawner = ServiceLocator.GetService<IEffectSpawner>();
    }
    private void OnEnable()
    {
        weaponMain.Shoted += SpawnShotEffect;
    }

    private void OnDisable()
    {
        weaponMain.Shoted -= SpawnShotEffect;
    }

    private void SpawnShotEffect()
    {
        effectSpawner.SpawnEffect(effectType, startPositionEffect);
    }
}