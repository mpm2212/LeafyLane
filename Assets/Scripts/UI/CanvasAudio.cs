using UnityEngine;
using UnityEngine.UI;

public class CanvasAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] buttonSounds;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioClip gameMusic;

    float musicVolume;
    float sfxVolume;

    bool isMusicMuted;
    bool isSFXMuted;

    void Start()
    {
        if (soundSlider != null) { soundSlider.value = 1f; }
        if (musicSlider != null) { musicSlider.value = 1f; }

        AudioEvents.RaisePlayMusic(gameMusic);

        isMusicMuted = false;
        isSFXMuted = false;

        musicVolume = musicSlider.value;
        sfxVolume = soundSlider.value;
    }

    public void PlayButtonClick()
    {
        if (buttonSounds != null)
        {
            int i = Random.Range(0, buttonSounds.Length);
            AudioEvents.RaisePlaySFX(buttonSounds[i]);
        }
    }

    public void UpdateMusicVolume()
    {
        if (musicSlider != null) { AudioEvents.RaiseSetMusicVolume(musicSlider.value); }
        Debug.Log("music slider value: " + musicSlider.value);
    }

    public void UpdateSoundVolume()
    {
        if (soundSlider != null) { AudioEvents.RaiseSetSFXVolume(soundSlider.value); }
        Debug.Log("sfx slider value: " + soundSlider.value);
    }

    public void SetMusicMuted()
    {
        isMusicMuted = !isMusicMuted;
        AudioEvents.RaiseSetMusicMutedEvent(isMusicMuted);
        if (isMusicMuted) { musicSlider.value = 0; }
        else { musicSlider.value = musicVolume; }
    }

    public void SetSFXMuted()
    {
        isSFXMuted = !isSFXMuted;
        AudioEvents.RaiseSetSFXMutedEvent(isSFXMuted);
        if (isSFXMuted) { soundSlider.value = 0; }
        //else{ soundSlider.value = 1; }
    }
}
