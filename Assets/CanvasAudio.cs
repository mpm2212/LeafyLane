using UnityEngine;

public class CanvasAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] buttonSounds;

    public void PlayButtonClick()
    {
        if (buttonSounds != null)
        {
            int i = Random.Range(0, buttonSounds.Length);
            AudioEvents.RaisePlaySFX(buttonSounds[i]);
        }
    }
}
