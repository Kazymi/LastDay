using System.Linq;
using UnityEngine;

public class SpriteContainer : MonoBehaviour, ISpriteContainer
{
    [SerializeField] private SpriteConfiguration[] spriteConfigurations;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<ISpriteContainer>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<ISpriteContainer>();
    }

    public Sprite GetSprite(SpriteType spriteType)
    {
        return spriteConfigurations.Where(t => t.SpriteType == spriteType).ToList()[0].Sprite;
    }
}