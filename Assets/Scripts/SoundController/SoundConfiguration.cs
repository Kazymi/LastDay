using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SoundConfiguration
{
    [SerializeField] private bool isAutoPlay;
    [SerializeField] private bool isRandomPlayer;
    [SerializeField] private bool isLoopPlayer;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private AudioSource source;

    private float startVolume;
    private bool isSoundPlayEnable = true;

    public void Initialize()
    {
        startVolume = source.volume;
        source.loop = isLoopPlayer;
        if (isAutoPlay)
        {
            Play();
        }
    }

    public void Play()
    {
        source.PlayOneShot(isRandomPlayer ? audioClip[Random.Range(0, audioClip.Length)] : audioClip[0]);
    }

    public void StopPlaying()
    {
        isSoundPlayEnable = false;
        source.volume = 0;
    }

    public void ReplaceSound(AudioClip audioClip)
    {
        if (isSoundPlayEnable == false)
        {
            this.audioClip[0] = audioClip;
        }
        else
        {
            var result = startVolume;
            DOTween.To(() => result, x => result = x, 0, 0.9f)
                .OnUpdate(() => { source.volume = result; }).OnComplete(() =>
                {
                    this.audioClip[0] = audioClip;
                    source.Stop();
                    Play();
                    var resultEnd = source.volume;
                    DOTween.To(() => resultEnd, x => resultEnd = x, startVolume, 0.9f)
                        .OnUpdate(() => { source.volume = resultEnd; });
                });
        }
    }

    public void PlaySound()
    {
        isSoundPlayEnable = true;
        source.volume = startVolume;
    }

    public void Tick()
    {
        if (isLoopPlayer)
        {
            if (source.isPlaying == false) Play();
        }
    }
}