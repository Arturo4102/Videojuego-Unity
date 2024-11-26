using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject volumeSlider, sensitivitySlider, toggleFullscreen;
    [SerializeField] private AudioMixer audioMixer;
    
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.GetComponent<Slider>().value = 0f;
        }
        else
        {
            volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
        }

        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivitySlider.GetComponent<Slider>().value = 25f;
        }
        else
        {
            sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");
        }

        if (!PlayerPrefs.HasKey("FullScreen"))
        {
            Screen.fullScreen = true;
            toggleFullscreen.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
            toggleFullscreen.GetComponent<Toggle>().isOn = Screen.fullScreen;
        }
    }

    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("FullScreen", Convert.ToInt32(fullscreen));
        PlayerPrefs.Save();
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
		PlayerPrefs.SetFloat("Volume", volume);
		PlayerPrefs.Save();
    }

    public void ChangeSensitivity(float sensitivity)
    {
        // check if there's a player controller, if so set the sensitivity
        if (FindObjectOfType<PlayerController>())
        {
            FindObjectOfType<PlayerController>().MouseSens = sensitivity;
        }
        else
        {
            //Debug.Log("No player controller found");
        }

        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }
}
