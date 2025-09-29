// code expanded on from Canvas exercise

using UnityEngine;
using System;


public static class AudioEvents
{
    public static event Action<AudioClip> PlaySFXEvent;
    public static event Action<AudioClip> PlayMusicEvent;
    public static event Action<bool> SetMutedEvent;
    public static event Action<float> SetMusicVolumeEvent;
    public static event Action<float> SetSFXVolumeEvent;
    public static event Action<bool> SetMusicMutedEvent;
    public static event Action<bool> SetSFXMutedEvent;



    public static void RaisePlaySFX(AudioClip clip) => PlaySFXEvent?.Invoke(clip);
    public static void RaisePlayMusic(AudioClip clip) => PlayMusicEvent?.Invoke(clip);
    public static void RaiseSetMuted(bool muted) => SetMutedEvent?.Invoke(muted);
    public static void RaiseSetMusicVolume(float volume) => SetMusicVolumeEvent?.Invoke(volume);
    public static void RaiseSetSFXVolume(float volume) => SetSFXVolumeEvent?.Invoke(volume);
    public static void RaiseSetSFXMutedEvent(bool muted) => SetSFXMutedEvent?.Invoke(muted);
    public static void RaiseSetMusicMutedEvent(bool muted) => SetMusicMutedEvent?.Invoke(muted);



}
