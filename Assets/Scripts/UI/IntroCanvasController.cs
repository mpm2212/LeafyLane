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
    [SerializeField] GameObject startGameButton;

    [Header("Nutty Sprites")]

    [SerializeField] Sprite nuttyIdle;
    [SerializeField] Sprite nuttyExcited;
    [SerializeField] Sprite nuttyCalm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nuttyImage = nutty.GetComponent<Image>();
        startGameButton.SetActive(false);
        StartCoroutine(introText());
    }

    void Update()
    {

    }

    /*
    Oh hey there, collector! Nice to see you. 
    
    Let me show you my favorite place, Leafy Lane.
    
    An island you have yet to explore - appealing, I know. Here, I set this up for you. There's a place to display your finds!

    I wonder what awaits you out there. Anywho, have fun!
    */

    IEnumerator introText()
    {
        nuttyImage.sprite = nuttyIdle;
        text.text = "Oh hey there, collector! Nice to see you.";
        yield return new WaitForSeconds(4);

        nuttyImage.sprite = nuttyCalm;
        text.text = "Let me show you my favorite place, Leafy Lane.";
        yield return new WaitForSeconds(4);

        nuttyImage.sprite = nuttyExcited;
        text.text = "An island you have yet to explore - appealing, I know. Here, I set this up for you. There's a place to display your finds!";
        yield return new WaitForSeconds(6);

        nuttyImage.sprite = nuttyIdle;
        text.text = "I wonder what awaits you out there. Anywho, have fun!";
        startGameButton.SetActive(true);
        yield return new WaitForSeconds(5);

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
