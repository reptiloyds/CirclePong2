using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundData : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup master;
    [SerializeField] private Image musicButton;
    [SerializeField] private Image soundButton;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    void Start()
    {
        #region SoundTuning
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetString("Sound", "On");
        }
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetString("Music", "On");
        }


        if (PlayerPrefs.GetString("Music") == "On")
        {
            musicButton.sprite = musicOn;
            master.audioMixer.SetFloat("MusicVolume", 0f);
        }
        else
        {
            musicButton.sprite = musicOff;
            master.audioMixer.SetFloat("MusicVolume", -80f);
        }
        if (PlayerPrefs.GetString("Sound") == "On")
        {
            soundButton.sprite = soundOn;
            master.audioMixer.SetFloat("SoundVolume", 0f);
        }
        else
        {
            soundButton.sprite = soundOff;
            master.audioMixer.SetFloat("SoundVolume", -80f);
        }
        #endregion
    }
    public void ToggleMusic()
    {
        if (PlayerPrefs.GetString("Music") == "On")
        {
            master.audioMixer.SetFloat("MusicVolume", -80f);
            PlayerPrefs.SetString("Music", "Off");
            musicButton.sprite = musicOff;
        }
        else
        {
            master.audioMixer.SetFloat("MusicVolume", 0f);
            PlayerPrefs.SetString("Music", "On");
            musicButton.sprite = musicOn;
        }
    }
    public void ToggleSound()
    {
        if (PlayerPrefs.GetString("Sound") == "On")
        {
            master.audioMixer.SetFloat("SoundVolume", -80f);
            PlayerPrefs.SetString("Sound", "Off");
            soundButton.sprite = soundOff;
        }
        else
        {
            master.audioMixer.SetFloat("SoundVolume", 0f);
            PlayerPrefs.SetString("Sound", "On");
            soundButton.sprite = soundOn;
        }
    }
}
