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

    }

    void Update()
    {

    }

    public void transitionToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void toggleAboutPanel() { aboutPanel.SetActive(!aboutPanel.activeSelf); }
    public void toggleMainMenu() { mainMenu.SetActive(!aboutPanel.activeSelf); }
    public void ExitGame(){ Application.Quit(); }

}

