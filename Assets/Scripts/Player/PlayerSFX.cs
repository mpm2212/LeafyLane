using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioClip[] grassWalkingSFX;

    public void PlayGrassWalk()
    {
        int i = Random.Range(0, grassWalkingSFX.Length);
        AudioEvents.RaisePlaySFX(grassWalkingSFX[i]);
    }
}
