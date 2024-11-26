using System;
using UnityEngine;
using UnityEngine.Audio;

public class InitMixerAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public AudioClip song;

    void Awake()
    {
        float volume = 0f;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        } 
        
        audioMixer.SetFloat("Volume", volume);
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("FullScreen", Convert.ToInt32(true));
        PlayerPrefs.Save();
        
    }
}
