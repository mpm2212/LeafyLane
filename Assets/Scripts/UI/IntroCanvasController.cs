using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCanvasController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject nutty;
    Image nuttyImage;
    [SerializeField] GameObject spaceToContinue;

    [Header("Nutty Sprites")]

    [SerializeField] Sprite nuttyIdle;
    [SerializeField] Sprite nuttyExcited;
    [SerializeField] Sprite nuttyCalm;
    bool UICoroutineRunning;

    void Start()
    {
        nuttyImage = nutty.GetComponent<Image>();
        StartCoroutine(introText());
        spaceToContinue.SetActive(true);
    }

    void Update()
    {
        if (!UICoroutineRunning && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            LoadGame();
        }
    }

    /*
    Oh hey there, collector! Nice to see you. 
    
    Let me show you my favorite place, Leafy Lane.
    
    An island you have yet to explore - appealing, I know. Here, I set this up for you. There's a place to display your finds!

    I wonder what awaits you out there. Anywho, have fun!
    */

    IEnumerator introText()
    {
        UICoroutineRunning = true;
        nuttyImage.sprite = nuttyIdle;
        text.text = "Oh hey there, collector! Nice to see you.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

        spaceToContinue.SetActive(false);
        nuttyImage.sprite = nuttyCalm;
        text.text = "Let me show you my favorite place, Leafy Lane.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));


        nuttyImage.sprite = nuttyExcited;
        text.text = "An island you have yet to explore - appealing, I know. Here, I set this up for you. There's a place to display your finds!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));


        nuttyImage.sprite = nuttyIdle;
        text.text = "I wonder what awaits you out there. Anywho, have fun!";
        spaceToContinue.SetActive(true);
        spaceToContinue.GetComponent<TextMeshProUGUI>().text = "Press space or enter to start the game";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

        UICoroutineRunning = false;
    }

    public void LoadGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
}
