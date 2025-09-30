using UnityEngine;

public class LightbulbPickup : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnEnable()
    {
        GameEvents.LightbulbPickedUpEvent += HandleLightbulbPickedup;
    }

    void OnDisable()
    {
        GameEvents.LightbulbPickedUpEvent -= HandleLightbulbPickedup;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameEvents.RaiseLightbulbPickedUpEvent(this.gameObject);
        }
    }

    void HandleLightbulbPickedup(GameObject bulb)
    {
        bulb.SetActive(false);
    }
}
