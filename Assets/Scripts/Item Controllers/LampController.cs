using UnityEngine;

public class LampController : MonoBehaviour
{
    GameObject player;
    [SerializeField] Sprite litSprite;
    [SerializeField] GameObject topHalf;
    [SerializeField] LayerMask pickupLayer;
    SpriteRenderer topHalfRenderer;
    bool lit;

    void Start()
    {
        lit = false;

        if (topHalf != null)
        {
            topHalfRenderer = topHalf.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E) && !lit && GameCanvasController.Instance.GetLightbulbsCollected() > 0)
        {
            Debug.Log("e pressed");
            GameEvents.RaiseLightBulbPlacedEvent(-1);
            lit = true;
            gameObject.layer = pickupLayer;
        }

    }

    void OnEnable()
    {
        GameEvents.LightbulbPlacedEvent += HandleLightbulbPlacedEvent;
    }

    void OnDisable()
    {
        GameEvents.LightbulbPlacedEvent -= HandleLightbulbPlacedEvent;
    }

    void HandleLightbulbPlacedEvent(int numLightbulbs)
    {
        if (player != null)
        {
            topHalfRenderer.sprite = litSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
        Debug.Log("player: " + player);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = null;
            Debug.Log("player: " + player);
        }
    }
}
