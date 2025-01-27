using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    private void Awake()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        volumeSlider.value = savedVolume;

        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        float mappedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;

        audioMixer.SetFloat("MasterVolume", mappedVolume);

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
