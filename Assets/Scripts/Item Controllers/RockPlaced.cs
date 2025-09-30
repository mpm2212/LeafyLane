using UnityEngine;

public class RockPlaced : MonoBehaviour
{
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
        if (other.gameObject.tag == "Meadow-2")
        {
            GameEvents.RaiseRockPlacedCorrectly(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Meadow-2")
        {
            GameEvents.RaiseRockPlacedCorrectly(false);
        }
    }
}
