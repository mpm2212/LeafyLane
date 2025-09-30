using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsPanel;
    void Start()
    {
        aboutPanel.SetActive(false);
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void transitionToGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public void toggleAboutPanel() { aboutPanel.SetActive(!aboutPanel.activeSelf); }
    public void toggleMainMenu() { mainMenu.SetActive(!mainMenu.activeSelf); }
    public void toggleSettingsPanel(){settingsPanel.SetActive(!settingsPanel.activeSelf); }
    public void ExitGame() { Application.Quit(); }

}

