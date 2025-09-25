using UnityEditor.PackageManager;
using UnityEngine;

// code from Singleton Audio Manager exercise in Canvas

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Volumes")]
    [Range(0f, 1f)][SerializeField] private float musicVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 1f;
    bool isMuted;

    void Awake()
    {
        AudioManagerSingleton();
        MakeMusicSourceLoopable();
        ApplyVolumes();
    }

    void AudioManagerSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void MakeMusicSourceLoopable()
    {
        musicSource.loop = true;
    }

    void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SetMuted(bool muted)
    {
        isMuted = muted;
        AudioListener.volume = muted ? 0f : 1f;

    }


}
