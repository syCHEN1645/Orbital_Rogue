using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider effectSlider;
    private string bgm = ManagerParameters.BGM;
    private string effect = ManagerParameters.EFFECT;
    private float coe = ManagerParameters.COE;

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

        if (PlayerPrefs.HasKey(effect)) {
            LoadEffectVolume();
        } else {
            SetEffectVolume();
        }
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

    public void SetEffectVolume() {
        // vol is the slider value, not the actual volume
        float vol = effectSlider.value;
        // music val changes logarithmically, slider val changes linearly
        mixer.SetFloat(effect, Mathf.Log10(vol) * coe);

        // PlayerPrefs Class: saves player's preferences
        // store vol (slider value) into PlayerPrefs
        PlayerPrefs.SetFloat(effect, vol);
    }

    private void LoadEffectVolume() {
        // get vol (slider value) from PlayerPrefs
        effectSlider.value = PlayerPrefs.GetFloat(effect);
        SetEffectVolume();
    }
}
