using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider VolumeSlider;
    public Slider SFXSlider;

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }
    public void SetMusicVolume()
    {
        float volume = VolumeSlider.value;
        Mixer.SetFloat("music", Mathf.Log10(volume)*20f);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        Mixer.SetFloat("sfx", Mathf.Log10(volume) * 20f);
    }
}
