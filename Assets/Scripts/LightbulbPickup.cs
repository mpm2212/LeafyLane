using UnityEngine;

public class LightbulbPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameEvents.RaiseLightbulbPickedUpEvent(1);
            gameObject.SetActive(false);
        }
    }
}
