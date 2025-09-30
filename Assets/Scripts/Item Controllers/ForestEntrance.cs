using UnityEngine;

public class ForestEntrance : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameEvents.RaisePlayerEnteredForestEvent(other.gameObject);
        }

        other.gameObject.GetComponent<ForestEntrance>().enabled = false;
    }
}
