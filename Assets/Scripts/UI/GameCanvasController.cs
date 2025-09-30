using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class GameCanvasController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] GameObject WASDPanel;
    [SerializeField] GameObject LShiftPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject regionUnlockedPanel;
    [SerializeField] GameObject flowersPanel;
    [SerializeField] GameObject flower1;
    [SerializeField] GameObject flower2;
    [SerializeField] GameObject lightbulbPanel;
    [SerializeField] TextMeshProUGUI lightbulbText;
    Image flower1Image;
    Image flower2Image;
    [SerializeField] TextMeshProUGUI regionUnlockedText;

    Coroutine WASDTimer;

    int hideTime;
    int numLightbulbsCollected;

    public static GameCanvasController Instance { get; private set; }

    void Awake()
    {
        GameCanvasControllerSingleton();
    }
    void Start()
    {
        WASDPanel.SetActive(true);
        LShiftPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        helpPanel.SetActive(false);
        regionUnlockedPanel.SetActive(false);
        flowersPanel.SetActive(true);
        lightbulbPanel.SetActive(false);

        flower1Image = flower1.GetComponent<Image>();
        flower2Image = flower2.GetComponent<Image>();

        flower1Image.color = Color.black;
        flower2Image.color = Color.black;

        numLightbulbsCollected = 0;

        hideTime = 4;

        WASDTimer = StartCoroutine(KeyExplanationTimer());
    }

    void Update()
    {

    }

    void OnEnable()
    {
        GameEvents.RegionUnlockedEvent += HandleRegionUnlocked;
        GameEvents.FlowerPlacedEvent += HandleFlowerPlacedUI;
    }

    void OnDisable()
    {
        GameEvents.RegionUnlockedEvent -= HandleRegionUnlocked;
        GameEvents.FlowerPlacedEvent -= HandleFlowerPlacedUI;
        GameEvents.LightbulbPickedUpEvent -= HandleLightbulbPickedUpUI;


    }

    void HandleRegionUnlocked(String region)
    {
        if (region == "Meadow-2") { flowersPanel.SetActive(false); }
        if (region == "Village") { lightbulbPanel.SetActive(true); }
    }

    void HandleFlowerPlacedUI(int totalNumFlowers, bool more)
    {
        Debug.Log("in flower placed ui receiver");
        if (more)
        {
            if (flower1Image.color == Color.black)
            {
                flower1Image.color = Color.white;
            }
            else if (flower2Image.color == Color.black)
            {
                flower2Image.color = Color.white;
            }
        }

        else
        {
            if (flower2Image.color == Color.white)
            {
                flower2Image.color = Color.black;
            }
            else if (flower1Image.color == Color.white)
            {
                flower1Image.color = Color.black;
            }
        }
    }

    void GameCanvasControllerSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator KeyExplanationTimer()
    {
        WASDPanel.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D));
        yield return new WaitForSeconds(5);

        WASDPanel.SetActive(false);
        LShiftPanel.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
        yield return new WaitForSeconds(5);

        LShiftPanel.SetActive(false);
    }

    IEnumerator ShowPanelForSeconds(GameObject panel, int seconds)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        panel.SetActive(false);
    }

    public void TogglePanel(GameObject panel)
    {
        if (panel != null) { panel.SetActive(!panel.activeInHierarchy); }
    }

    public void ToggleMainMenu()
    {
        TogglePanel(mainMenuPanel);
        if (mainMenuPanel.activeInHierarchy) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void ExitGame() { Application.Quit(); }

    public void ShowRegionUnlocked(String text)
    {
        regionUnlockedText.text = text;
        StartCoroutine(ShowPanelForSeconds(regionUnlockedPanel, hideTime));
    }

    void HandleLightbulbPickedUpUI(GameObject obj)
    {
        lightbulbText.text = numLightbulbsCollected.ToString();
    }


}
