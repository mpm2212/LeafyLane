using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class GameIntroCanvasController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject WASDPanel;
    [SerializeField] GameObject LShiftPanel;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;

    Coroutine WASDTimer;
    Coroutine LShiftTimer;
    [SerializeField] float hideTime;

    void Start()
    {
        WASDPanel.SetActive(true);
        LShiftPanel.SetActive(false);
        helpPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (WASDPanel.activeInHierarchy && WASDTimer == null)
            {
                WASDTimer = StartCoroutine(HelpTimer(WASDPanel));
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !LShiftPanel.activeInHierarchy && !WASDPanel.activeInHierarchy && LShiftTimer == null)
        {
            LShiftPanel.SetActive(true);
            StartCoroutine(HelpTimer(LShiftPanel));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { ToggleMainMenu(); }
    }


    IEnumerator IFadeIn(Image panelImage)
    {
        Color panelColor = panelImage.color;

        while (panelColor.a < 1f)
        {
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, panelColor.a + (Time.deltaTime / hideTime));
            yield return null;

        }
    }

    IEnumerator HelpTimer(GameObject panel)
    {
        yield return new WaitForSeconds(hideTime);

        if (LShiftPanel.activeInHierarchy == false)
        {
            LShiftPanel.SetActive(true);
        }

        panel.SetActive(false);

    }

    public void ToggleHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
    }

    public void ToggleMainMenu()
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeInHierarchy);
        if (mainMenuPanel.activeInHierarchy) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void ExitGame() { Application.Quit(); }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }


}
