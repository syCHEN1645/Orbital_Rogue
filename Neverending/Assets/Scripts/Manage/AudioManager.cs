using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    public AudioSource bgmSource;
    public AudioSource effectSource;

    [Header("----------Audio Clip----------")]
    public AudioClip theme;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;

    // void Start() {
    //     if (bgmSource.clip == null) {
    //         // 
    //     }
    //     bgmSource.Play();
    // }

    public void PlayEffectSounds(AudioClip clip) {
        effectSource.PlayOneShot(clip);
    }
}
