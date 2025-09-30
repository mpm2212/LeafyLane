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
        GameEvents.RockPlacedCorrectlyEvent += HandleRockPlacedCorrectly;
    }

    void OnDisable()
    {
        GameEvents.RockPlacedCorrectlyEvent -= HandleRockPlacedCorrectly;

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
