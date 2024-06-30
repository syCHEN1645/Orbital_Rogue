using UnityEngine;
using UnityEngine.Audio;

public class SettingsImporter : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private AudioMixer mixer;
    private string bgm = ManagerParameters.BGM;
    private string effect = ManagerParameters.EFFECT;
    private float coe = ManagerParameters.COE;

    void Start()
    {
        if (PlayerPrefs.HasKey(bgm)) {
            float vol = PlayerPrefs.GetFloat(bgm);
            mixer.SetFloat(bgm, Mathf.Log10(vol) * coe);
        } else {
            mixer.SetFloat(bgm, 0);
        }

        if (PlayerPrefs.HasKey(effect)) {
            float vol = PlayerPrefs.GetFloat(effect);
            mixer.SetFloat(effect, Mathf.Log10(vol) * coe);
        } else {
            mixer.SetFloat(effect, 0);
        }
    }
}
