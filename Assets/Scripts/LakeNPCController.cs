using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LakeNPCController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private bool BobFound = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger is activated");
        if(other.gameObject.tag == "Player"){
            Debug.Log("Player has been Detected");

            text.text =  
            "Can you help me? I think my Bob ran away and I can't seem to find him. " + 
            "If you spot him, don’t mention the blob thing — he’ll sulk for hours. " +
            "Just tell him I promised snacks, that usually does the trick.";
            text.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
        }

    }

    /*
    Can you help me? I think my Bob ran away and I can't seem to find him. 
    If you spot him, don’t mention the blob thing — he’ll sulk for hours. Just 
    tell him I promised snacks, that usually does the trick.
    */
}
