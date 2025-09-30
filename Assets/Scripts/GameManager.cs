using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool meadow2Unlocked;
    bool villageUnlocked;
    bool forestUnlocked;
    bool lakeUnlocked;

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
        meadow2Unlocked = false;
        villageUnlocked = false;
        forestUnlocked = false;
        lakeUnlocked = false;

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
        GameEvents.RemoveCloudsEvent += HandleRemoveClouds;
        GameEvents.RegionUnlockedEvent += HandleRegionUnlocked;
    }



    void OnDisable()
    {
        GameEvents.RemoveCloudsEvent -= HandleRemoveClouds;
        GameEvents.RegionUnlockedEvent -= HandleRegionUnlocked;

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
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the east has been unlocked...");
                return;
            case ("Lake"):
            case (""):
                GameEvents.RaiseRemoveClouds(lakeClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the south has been unlocked...");
                return;

            case ("Village"):
                GameEvents.RaiseRemoveClouds(villageClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the west has been unlocked...");
                return;
            case ("Forest"):
                GameEvents.RaiseRemoveClouds(forestClouds);
                GameCanvasController.Instance.ShowRegionUnlocked("A region to the west has been unlocked...");
                return;
    }
    }

}
