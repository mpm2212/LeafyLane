using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Button playButton;
    void Start()
    {

    }

    void Update()
    {

    }

    public void transitionToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
