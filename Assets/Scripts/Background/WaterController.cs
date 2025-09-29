using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float pushForce = 1.0f;
    private PlayerMovement.WalkingDirection direction;
    private Vector3 offset = new Vector3(0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Hit Water!");
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Push Player");
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushDir = other.contacts[0].normal;
            //rb.linearVelocity = pushDir * pushForce;
            rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }

}
