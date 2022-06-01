using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioMixer sfxAudioMixer;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] Toggle muteToggle;


    public void SetVolumeMaster(float volume)
    {
        masterMixer.SetFloat("volume", volume);
        sfxAudioMixer.SetFloat("volume", volume);
        musicMixer.SetFloat("volume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        sfxAudioMixer.SetFloat("volume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("volume", volume);
    }

    public void MuteOrUnmute()
    {
        if (muteToggle.isOn)
        {
            masterMixer.SetFloat("volume", -60f);
            sfxAudioMixer.SetFloat("volume", -60f);
            musicMixer.SetFloat("volume", -60f);
        }
        else
        {
            masterMixer.SetFloat("volume", 0f);
            sfxAudioMixer.SetFloat("volume", 0f);
            musicMixer.SetFloat("volume", 0f);
        }
    }
}
