using UnityEngine;

public class CharacterShopAnimatorActivator : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private CharacterShopAnimationType _shopAnimationType;

    private void OnEnable()
    {
        characterAnimator.SetTrigger(_shopAnimationType.ToString());
    }
}