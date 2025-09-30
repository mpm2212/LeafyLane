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
    /* HI JORGE AND BENNO

    I know this area is a bit of a mess. Trust, it's not my favorite.
    If I had more time you bet I would have organized the UI controls better. But alas, we were pressed for time.

    Happy grading,
    Ellie

    */

    [Header("Tooltip Panels")]

    [SerializeField] GameObject WASDPanel;
    [SerializeField] GameObject LShiftPanel;
    [SerializeField] GameObject lampTooltipPanel;

    [Header("UI Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject regionUnlockedPanel;
    [SerializeField] GameObject flowersPanel;
    [SerializeField] GameObject flower1;
    [SerializeField] GameObject flower2;
    [SerializeField] GameObject lightbulbPanel;
    [SerializeField] TextMeshProUGUI lightbulbText;
    [SerializeField] TextMeshProUGUI lampText;
    [SerializeField] GameObject freddyPanel;
    [SerializeField] TextMeshProUGUI freddyText;
    [SerializeField] GameObject spaceToContinue;
    [SerializeField] GameObject timerPanel;
    [SerializeField] GameObject finishedPanel;
    Image flower1Image;
    Image flower2Image;
    [SerializeField] TextMeshProUGUI regionUnlockedText;

    Coroutine WASDTimer;
    bool UICoroutineRunning;
    bool talkedToFreddy;

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
        freddyPanel.SetActive(false);
        lampTooltipPanel.SetActive(false);
        finishedPanel.SetActive(false);

        flower1Image = flower1.GetComponent<Image>();
        flower2Image = flower2.GetComponent<Image>();

        flower1Image.color = Color.black;
        flower2Image.color = Color.black;

        numLightbulbsCollected = 0;
        UpdateLampText(VillageManager.Instance.GetNumLampsLeft());

        hideTime = 4;

        WASDTimer = StartCoroutine(KeyExplanationTimer());

        talkedToFreddy = false;
    }

    void OnEnable()
    {
        GameEvents.RegionUnlockedEvent += HandleRegionUnlocked;
        GameEvents.FlowerPlacedEvent += HandleFlowerPlacedUI;
        GameEvents.LightbulbPickedUpEvent += HandleLightbulbPickedUpUI;
        GameEvents.LightbulbPlacedEvent += HandleLightbulbPlacedEvent;
        GameEvents.TalkingToFreddyEvent += HandleTalkingToFreddyUI;
        GameEvents.FoundBobEvent += HandleFoundBobEvent;
        GameEvents.DidntFindBobEvent += HandleDidntFindBobEvent;
        GameEvents.PlayerEnteredForestEvent += HandlePlayerEnteredForestEvent;

    }

    void OnDisable()
    {
        GameEvents.RegionUnlockedEvent -= HandleRegionUnlocked;
        GameEvents.FlowerPlacedEvent -= HandleFlowerPlacedUI;
        GameEvents.LightbulbPickedUpEvent -= HandleLightbulbPickedUpUI;
        GameEvents.LightbulbPlacedEvent -= HandleLightbulbPlacedEvent;
        GameEvents.TalkingToFreddyEvent -= HandleTalkingToFreddyUI;
        GameEvents.FoundBobEvent -= HandleFoundBobEvent;
        GameEvents.DidntFindBobEvent -= HandleDidntFindBobEvent;
        GameEvents.PlayerEnteredForestEvent -= HandlePlayerEnteredForestEvent;

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

    IEnumerator ShowPanelUntilKeyPress(GameObject panel, int seconds)
    {
        panel.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));
        panel.SetActive(false);
    }

    public void ToggleLampTooltip()
    {
        StartCoroutine(ShowPanelForSeconds(lampTooltipPanel, hideTime));
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

    void HandleLightbulbPickedUpUI(int i)
    {
        numLightbulbsCollected++;
        UpdateLightbulbText(numLightbulbsCollected);
        Debug.Log("updated lightbulb text: " + numLightbulbsCollected);
    }

    void HandleLightbulbPlacedEvent(int i)
    {
        numLightbulbsCollected--;
        UpdateLightbulbText(numLightbulbsCollected);

        UpdateLampText(VillageManager.Instance.GetNumLampsLeft());

    }

    public int GetLightbulbsCollected()
    {
        return numLightbulbsCollected;
    }

    public void UpdateLightbulbText(int numLightbulbs)
    {
        lightbulbText.text = numLightbulbsCollected.ToString();
    }

    void UpdateLampText(int numLamp)
    {
        lampText.text = numLamp.ToString();
    }

    IEnumerator ITalkingToFreddy()
    {
        UICoroutineRunning = true;
        freddyPanel.SetActive(true);

        //nuttyImage.sprite = nuttyIdle;
        freddyText.text = "Can you help me? I think my Bob, er that is, Bob the Blob, ran away and I can't seem to find him.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

        spaceToContinue.SetActive(false);
        //nuttyImage.sprite = nuttyCalm;
        freddyText.text = "If you spot him, don’t mention the blob thing — he’ll sulk for hours. ";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));


        //nuttyImage.sprite = nuttyExcited;
        freddyText.text = "Just tell him I promised snacks, that usually does the trick.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

        UICoroutineRunning = false;
        freddyPanel.SetActive(false);
        talkedToFreddy = true;
    }

    IEnumerator IFreddySingleLiner(String text)
    {
        freddyPanel.SetActive(true);
        freddyText.text = text;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

        freddyPanel.SetActive(false);

    }

    void HandleTalkingToFreddyUI(GameObject obj)
    {
        if (!talkedToFreddy)
        {
            Debug.Log("in handle talking to freddy UI");
            StartCoroutine(ITalkingToFreddy());
        }
    }

    void HandleFoundBobEvent(GameObject obj)
    {
        StartCoroutine(IFreddySingleLiner("Wow, thanks for finding Bob! I think he likes you... he might follow you from now on :)"));
        timerPanel.SetActive(false);
    }

    void HandleDidntFindBobEvent(GameObject obj)
    {
        StartCoroutine(IFreddySingleLiner("Hey, it took you too long and you lost Bob. What the heck! Now he's run away to a different location"));
    }

    void HandlePlayerEnteredForestEvent(GameObject obj)
    {
        StartCoroutine(ShowPanelUntilKeyPress(finishedPanel, hideTime));
    }

}
