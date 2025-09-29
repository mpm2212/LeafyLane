using UnityEngine;

public class CanvasAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] buttonSounds;

    public void PlayButtonClick()
    {
        if (buttonSounds != null)
        {
            int i = Random.Range(0, buttonSounds.Length);
            AudioManager.Instance.PlaySFX(buttonSounds[i]);
            Debug.Log("Played button click with clip " + buttonSounds[i]);
            //Debug.Log("volume = " + AudioManager.Instance.sfx)
        }
    }
}
