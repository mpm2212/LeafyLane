using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject mainMenu;
    void Start()
    {
        aboutPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void transitionToGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public void toggleAboutPanel() { aboutPanel.SetActive(!aboutPanel.activeSelf); }
    public void toggleMainMenu() { mainMenu.SetActive(!mainMenu.activeSelf); }
    public void ExitGame() { Application.Quit(); }

}

