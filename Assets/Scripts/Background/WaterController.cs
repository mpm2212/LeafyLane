using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;

    private WalkingDirection direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Hit Water -> Push Player");
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushDir = other.contacts[0].normal;


            rb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
            Debug.Log("Player pos after: " + rb.position);
        }
    }

}
