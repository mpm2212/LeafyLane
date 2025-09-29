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
        if (fulfilledMeadowRequirements)
        {
            HandleRemoveClouds(meadowClouds);
        }

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
    }



    void OnDisable()
    {
        GameEvents.RemoveClouds -= HandleRemoveClouds;
    }

    void HandleRemoveClouds(GameObject cloudsToRemove)
    {
        Destroy(cloudsToRemove);
    }
}
