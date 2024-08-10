using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    [SerializeField] private TMP_Text sfxText, musicText;

    public static AudioManager Instance { get; private set;}

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip audioClip, float volume) {
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayMusic(AudioClip audioClip, float volume) {
        musicAudioSource.PlayOneShot(audioClip, volume);
    }

    public void ToggleMusic() {
        musicAudioSource.mute = !musicAudioSource.mute;

        if (musicAudioSource.mute) {
            musicText.text = "Music: off";
        } else {
            musicText.text = "Music: on";
        }
    }

    public void ToggleSFX() {
        sfxAudioSource.mute = !sfxAudioSource.mute;

        if (sfxAudioSource.mute) {
            sfxText.text = "SFX: off";
        } else {
            sfxText.text = "SFX: on";
        }
    }
}