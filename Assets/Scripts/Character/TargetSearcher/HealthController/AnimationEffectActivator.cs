using UnityEngine;

public class AnimationEffectActivator : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    public void Play()
    {
        particleSystem.Play();
    }
}