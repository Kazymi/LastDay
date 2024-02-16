
using System.Linq;
using UnityEngine;

public class SoundSystem : MonoBehaviour, ISoundSystem
{
    [SerializeField] private SoundConstructor[] constructors;

    public SoundConfiguration GetSound(SoundType soundType)
    {
        var result = constructors.Where(t => t.SoundType == soundType).ToList()[0].SoundConfiguration;
        return result;
    }

    public void UpdateSound()
    {
        SaveData.Instance.isSoundAcivated = !SaveData.Instance.isSoundAcivated;
        CheckSound();
    }

    private void Start()
    {
        OnInit();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<ISoundSystem>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<ISoundSystem>();
    }

    private void CheckSound()
    {
        if (SaveData.Instance.isSoundAcivated == false)
        {
            foreach (var constructor in constructors)
            {
                constructor.SoundConfiguration.StopPlaying();
            }
        }
        else
        {
            foreach (var constructor in constructors)
            {
                constructor.SoundConfiguration.PlaySound();
            }
        }
    }

    private void Update()
    {
        foreach (var constructor in constructors)
        {
            constructor.SoundConfiguration.Tick();
        }
    }

    public void OnInit()
    {
        foreach (var soundConstructor in constructors)
        {
            soundConstructor.SoundConfiguration.Initialize();
        }

        CheckSound();
    }

    public SoundConfiguration PlaySound(SoundType soundType)
    {
        var result = constructors.Where(t => t.SoundType == soundType).ToList()[0].SoundConfiguration;
        result.Play();
        return result;
    }
}