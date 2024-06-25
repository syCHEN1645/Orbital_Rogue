using System.Collections;
using System.Collections.Generic;
using Unity.CodeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider bgmSlider;
    private float coe = 20;

    void Start () {
        if (PlayerPrefs.HasKey("bgmVol")) {
            LoadVolume();
        } else {
            // make sure slider val and music vol matches at the start
            SetAudioVolume();
        }
    }
    public void SetAudioVolume() {
        float vol = bgmSlider.value;
        // music val changes logarithmically, slider val changes linearly
        mixer.SetFloat("bgm", Mathf.Log10(vol) * coe);

        // PlayerPrefs Class: saves player's preferences
        PlayerPrefs.SetFloat("bgmVol", vol);
    }

    private void LoadVolume() {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVol");
        SetAudioVolume();
    }
}
