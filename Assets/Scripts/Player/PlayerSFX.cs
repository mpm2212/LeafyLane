using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioClip[] grassWalkingSFX;
    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayGrassWalk()
    {
        int i = Random.Range(0, grassWalkingSFX.Length);
        AudioManager.Instance.PlaySFX(grassWalkingSFX[i]);
    }
}
