using UnityEngine;

public class VillageManager : MonoBehaviour
{

    public static VillageManager Instance { get; private set; }
    int numLightbulbsCollected;
    int numLampsLit;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        numLampsLit = 8;
        numLightbulbsCollected = 0;
    }


    void OnEnable()
    {
        GameEvents.LightbulbPlacedEvent += HandleLightbulbPlacedEvent;
        //GameEvents.LightbulbPickedUpEvent += HandleLightbulbPickedUpEvent;
    }

    void OnDisable()
    {
        GameEvents.LightbulbPlacedEvent -= HandleLightbulbPlacedEvent;
        //GameEvents.LightbulbPickedUpEvent += HandleLightbulbPickedUpEvent;
    }

    void HandleLightbulbPlacedEvent(int i)
    {
        numLampsLit--;
        Debug.Log("num lamps lit: " + numLampsLit);

        if (numLampsLit == 0)
        {
            GameEvents.RaiseRegionUnlocked("Forest");
        }

    }

    // void HandleLightbulbPickedUpEvent(int i)
    // {
    //     Debug.Log("in handlelightbuld pick up in villagemanager");
    // }

    public int GetNumLightbulbs()
    {
        return numLightbulbsCollected;
    }

    public int GetNumLampsLeft()
    {
        return numLampsLit;
    }
}
