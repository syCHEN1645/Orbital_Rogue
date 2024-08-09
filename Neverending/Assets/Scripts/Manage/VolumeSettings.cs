using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioManager manager;
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private TMP_Dropdown bgmDrop;
    // [SerializeField]
    // private Slider effectSlider;
    private string bgm = ManagerParameters.BGM;
    // private string effect = ManagerParameters.EFFECT;
    private float coe = ManagerParameters.COE;
    private string theme = ManagerParameters.THEME;

    void Start() {
        Initialise();
    }

    public void Initialise () {
        if (PlayerPrefs.HasKey(bgm)) {
            LoadBGMVolume();
        } else {
            // make sure slider val and music vol matches at the start
            SetBGMVolume();
        }

        if (PlayerPrefs.HasKey(theme)) {
            LoadBGMTheme();
        } else {
            SetBGMTheme();
        }

        // if (PlayerPrefs.HasKey(effect)) {
        //     LoadEffectVolume();
        // } else {
        //     SetEffectVolume();
        // }
    }

    public void OptionChanged() {
        PlayerPrefs.SetInt(theme, bgmDrop.value);
        LoadBGMTheme();
    }

    private void SetBGMTheme()
    {
        // if no saved settings, set to 0
        PlayerPrefs.SetInt(theme, 0);
        bgmDrop.value = 0;
        manager.bgmSource.Stop();
        manager.bgmSource.Play();
    }

    private void LoadBGMTheme()
    {
        switch (PlayerPrefs.GetInt(theme)) {
            case 0:
                manager.bgmSource.clip = manager.clip1;
                bgmDrop.value = 0;
                break;
            case 1:
                manager.bgmSource.clip = manager.clip2;
                bgmDrop.value = 1;
                break;
            case 2:
                manager.bgmSource.clip = manager.clip3;
                bgmDrop.value = 2;
                break;
            default:
                Debug.Log("invalid theme number");
                break;
        }
        manager.bgmSource.Stop();
        manager.bgmSource.Play();
    }

    public void SetBGMVolume() {
        // vol is the slider value, not actual volume
        float vol = bgmSlider.value;
        // music val changes logarithmically, slider val changes linearly
        mixer.SetFloat(bgm, Mathf.Log10(vol) * coe);

        // PlayerPrefs Class: saves player's preferences
        // stored value is vol (slider value), not actual vol
        PlayerPrefs.SetFloat(bgm, vol);
    }

    private void LoadBGMVolume() {
        bgmSlider.value = PlayerPrefs.GetFloat(bgm);
        SetBGMVolume();
    }

    // public void SetEffectVolume() {
    //     // vol is the slider value, not the actual volume
    //     float vol = effectSlider.value;
    //     // music val changes logarithmically, slider val changes linearly
    //     mixer.SetFloat(effect, Mathf.Log10(vol) * coe);

    //     // PlayerPrefs Class: saves player's preferences
    //     // store vol (slider value) into PlayerPrefs
    //     PlayerPrefs.SetFloat(effect, vol);
    // }

    // private void LoadEffectVolume() {
    //     // get vol (slider value) from PlayerPrefs
    //     effectSlider.value = PlayerPrefs.GetFloat(effect);
    //     SetEffectVolume();
    // }
}
