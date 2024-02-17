using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    private Button _button;
    private ISoundSystem soundSystem;

    void Start()
    {
        soundSystem = ServiceLocator.GetService<ISoundSystem>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Play);
    }

    private void Play()
    {
        soundSystem.PlaySound(SoundType.ButtonClick);
    }
}

public interface ISoundSystem
{
    SoundConfiguration PlaySound(SoundType soundType);
    SoundConfiguration GetSound(SoundType soundType);
    void UpdateSound();
}