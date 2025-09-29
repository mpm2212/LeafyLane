// code from Canvas exercise

using UnityEngine;
using System;


public static class AudioEvents
{
    public static event Action<AudioClip> PlaySFXEvent;
    public static event Action<AudioClip> PlayMusicEvent;
    public static event Action<bool> SetMutedEvent;

    public static void RaisePlaySFX(AudioClip clip) => PlaySFXEvent?.Invoke(clip);
    public static void RaisePlayMusic(AudioClip clip) => PlayMusicEvent?.Invoke(clip);
    public static void RaiseSetMuted(bool muted) => SetMutedEvent?.Invoke(muted);



}
