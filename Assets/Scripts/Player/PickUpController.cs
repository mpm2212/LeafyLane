using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public Transform holdSpot;
    public LayerMask pickupMask;
    public Vector3 Direction{get; set;}
    public float radius = 0.4f;

    private GameObject itemHolding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Direction = Vector2.right;
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(itemHolding){

            }else
            {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, radius, pickupMask);
                if (pickUpItem)
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    if(itemHolding.GetComponent<Rigidbody2D>()){
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                    }


                }
            }
        }
        
    }
}
