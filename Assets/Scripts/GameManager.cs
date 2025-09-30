using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool fulfilledMeadowRequirements;
    bool fulfilledVillageRequirements;
    bool fulfilledForestRequirements;
    bool fulfilledLakeRequirements;

    [Header("Cloud GameObjects")]
    [SerializeField] GameObject meadowClouds;
    [SerializeField] GameObject villageClouds;
    [SerializeField] GameObject lakeClouds;
    [SerializeField] GameObject forestClouds;

    void Awake()
    {
        GameManagerSingleton();
    }

    void Start()
    {
        fulfilledMeadowRequirements = false;
        fulfilledVillageRequirements = false;
        fulfilledForestRequirements = false;
        fulfilledLakeRequirements = false;

        meadowClouds.SetActive(true);
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
        Destroy(cloudsToRemove);
    }

    void HandleRegionUnlocked(String region)
    {
        switch (region)
        {
            case ("Meadow-2"):
                GameEvents.RaiseRemoveClouds(meadowClouds);
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
        fulfilledMeadowRequirements = meadowRequirements;
        if (fulfilledMeadowRequirements)
        {
            GameEvents.RaiseRegionUnlocked("Meadow-2");
        }
    }
}
