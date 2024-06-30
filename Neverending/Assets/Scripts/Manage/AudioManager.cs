using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource effectSource;

    [Header("----------Audio Clip----------")]
    public AudioClip theme;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;

    void Start() {
        bgmSource.clip = theme;
        bgmSource.Play();
    }

    public void PlayEffectSounds(AudioClip clip) {
        effectSource.PlayOneShot(clip);
    }
}
