using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool fulfilledMeadow2Requirements;
    bool fulfilledVillageRequirements;
    bool fulfilledForestRequirements;
    bool fulfilledLakeRequirements;

    [Header("Cloud GameObjects")]
    [SerializeField] GameObject meadow2Clouds;
    [SerializeField] GameObject villageClouds;
    [SerializeField] GameObject lakeClouds;
    [SerializeField] GameObject forestClouds;

    void Awake()
    {
        GameManagerSingleton();
    }

    void Start()
    {
        fulfilledMeadow2Requirements = false;
        fulfilledVillageRequirements = false;
        fulfilledForestRequirements = false;
        fulfilledLakeRequirements = false;

        meadow2Clouds.SetActive(true);
        villageClouds.SetActive(true);
        lakeClouds.SetActive(true);
        forestClouds.SetActive(true);
    }

    void Update()
    {

    }

    void GameManagerSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameEvents.RemoveClouds += HandleRemoveClouds;
        GameEvents.RegionUnlocked += HandleRegionUnlocked;
    }



    void OnDisable()
    {
        GameEvents.RemoveClouds -= HandleRemoveClouds;
        GameEvents.RegionUnlocked -= HandleRegionUnlocked;

    }

    void HandleRemoveClouds(GameObject cloudsToRemove)
    {
        if (cloudsToRemove != null) { Destroy(cloudsToRemove); }
    }

    void HandleRegionUnlocked(String region)
    {
        switch (region)
        {
            case ("Meadow-2"):
                GameEvents.RaiseRemoveClouds(meadow2Clouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the west has been unlocked...");
                return;
            case ("Lake"):
                GameEvents.RaiseRemoveClouds(lakeClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the south has been unlocked...");
                return;

            case ("Village"):
                GameEvents.RaiseRemoveClouds(villageClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the east has been unlocked...");
                return;
            case ("Forest"):
                GameEvents.RaiseRemoveClouds(forestClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the west has been unlocked...");
                return;
    }
    }

    public void setMeadowRequirements(bool meadowRequirements)
    {
        fulfilledMeadow2Requirements = meadowRequirements;
        if (fulfilledMeadow2Requirements)
        {
            GameEvents.RaiseRegionUnlocked("Meadow-2");
        }
    }
}
