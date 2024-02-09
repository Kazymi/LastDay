public interface ICharacterAnimationController
{
    void SetPlay(CharacterAnimationType characterAnimationType, bool isTrigger = false, int layer = 0,
        float time = 0);

    void SetFloat(CharacterAnimationType characterAnimationType, float value);
    void SetBool(CharacterAnimationType characterAnimationType, bool value);
}