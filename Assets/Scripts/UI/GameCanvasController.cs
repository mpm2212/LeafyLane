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
    [Header("Panels")]
    [SerializeField] GameObject WASDPanel;
    [SerializeField] GameObject LShiftPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject regionUnlockedPanel;
    [SerializeField] TextMeshProUGUI regionUnlockedText;

    Coroutine WASDTimer;

    [SerializeField] int hideTime;

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
        regionUnlockedPanel.SetActive(false);

        hideTime = 4;

        WASDTimer = StartCoroutine(KeyExplanationTimer());
    }

    void Update()
    {

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



}
