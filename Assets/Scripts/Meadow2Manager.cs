using UnityEngine;

public class Meadow2Manager : MonoBehaviour
{
    int numRocksPlaced;
    [SerializeField] AudioClip rockPlacedCorrectlyAudio;

    void Start()
    {
        numRocksPlaced = 0;
    }

    void Update()
    {

    }

    void OnEnable()
    {
        GameEvents.RockPlacedCorrectly += HandleRockPlacedCorrectly;
    }

    void OnDisable()
    {
        GameEvents.RockPlacedCorrectly -= HandleRockPlacedCorrectly;

    }

    void HandleRockPlacedCorrectly(bool correct)
    {
        if (correct)
        {
            numRocksPlaced++;
                AudioEvents.RaisePlaySFX(rockPlacedCorrectlyAudio);
        }
        else { numRocksPlaced--; }

        if (numRocksPlaced >= 13)
        {
            GameEvents.RaiseRegionUnlocked("Lake");
        }
        
        Debug.Log("num rocks placed: " + numRocksPlaced);
    }
}
