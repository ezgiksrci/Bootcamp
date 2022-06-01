using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioMixer sfxAudioMixer;
    [SerializeField] AudioMixer musicMixer;
    public void SetVolumeMaster(float volume)
    {
        masterMixer.SetFloat("volume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        sfxAudioMixer.SetFloat("volume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("volume", volume);
    }
}
