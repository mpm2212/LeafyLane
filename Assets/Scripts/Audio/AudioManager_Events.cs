// code from Canvas exercise

using UnityEngine;

public class AudioManager_Events : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)][SerializeField] private float musicVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 1f;

    void Awake()
    {
        if (musicSource == null) { musicSource = gameObject.AddComponent<AudioSource>(); }
        if (sfxSource == null) { sfxSource = gameObject.AddComponent<AudioSource>(); }
        musicSource.loop = true;
    }

    void OnEnable()
    {
        AudioEvents.PlaySFXEvent += HandlePlaySFX;
        AudioEvents.PlayMusicEvent += HandlePlayMusic;
        AudioEvents.SetMutedEvent += HandleSetMuted;
    }

    void OnDisable()
    {
        AudioEvents.PlaySFXEvent -= HandlePlaySFX;
        AudioEvents.PlayMusicEvent -= HandlePlayMusic;
        AudioEvents.SetMutedEvent -= HandleSetMuted;
    }

    void HandlePlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    void HandlePlayMusic(AudioClip clip)
    {
        if (clip == null) { return; }
        musicSource.PlayOneShot(clip, musicVolume);
    }

    void HandleSetMuted(bool muted) { AudioListener.volume = muted ? 0f : 1f; }


}
