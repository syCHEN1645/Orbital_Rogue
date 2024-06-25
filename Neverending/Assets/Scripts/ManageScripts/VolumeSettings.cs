using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private float coe = 20;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider effectSlider;
    const string BGM = "bgm";
    const string EFFECT = "effect";

    void Start () {
        if (PlayerPrefs.HasKey(BGM)) {
            LoadBGMVolume();
        } else {
            // make sure slider val and music vol matches at the start
            SetBGMVolume();
        }

        if (PlayerPrefs.HasKey(EFFECT)) {
            LoadEffectVolume();
        } else {
            SetEffectVolume();
        }
    }
    public void SetBGMVolume() {
        // vol is the slider value, not the actual volume
        float vol = bgmSlider.value;
        // music val changes logarithmically, slider val changes linearly
        mixer.SetFloat(BGM, Mathf.Log10(vol) * coe);

        // PlayerPrefs Class: saves player's preferences
        PlayerPrefs.SetFloat(BGM, vol);
    }

    private void LoadBGMVolume() {
        bgmSlider.value = PlayerPrefs.GetFloat(BGM);
        SetBGMVolume();
    }

    public void SetEffectVolume() {
        // vol is the slider value, not the actual volume
        float vol = effectSlider.value;
        // music val changes logarithmically, slider val changes linearly
        mixer.SetFloat(EFFECT, Mathf.Log10(vol) * coe);

        // PlayerPrefs Class: saves player's preferences
        PlayerPrefs.SetFloat(EFFECT, vol);
    }

    private void LoadEffectVolume() {
        effectSlider.value = PlayerPrefs.GetFloat(EFFECT);
        SetEffectVolume();
    }
}
