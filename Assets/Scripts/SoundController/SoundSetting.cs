using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite enable;
    [SerializeField] private Sprite disable;

    private bool isVisible;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        UpdateImage();
        soundButton.onClick.AddListener(UpdateSound);
        settingButton.onClick.AddListener(UpdateVisible);
    }

    private void UpdateImage()
    {
        soundImage.sprite = SaveData.Instance.isSoundAcivated ? enable : disable;
    }

    private void UpdateSound()
    {
        ServiceLocator.GetService<ISoundSystem>().UpdateSound();
        UpdateImage();
    }

    private void UpdateVisible()
    {
        if (isVisible)
        {
            foreach (var image in images)
            {
                image.DOFade(0, 0);
            }

            soundButton.interactable = false;
        }
        else
        {
            foreach (var image in images)
            {
                image.DOFade(1, 0);
            }

            soundButton.interactable = true;
        }

        isVisible = !isVisible;
    }
}