using UnityEngine;

public class PickUpController : MonoBehaviour
{

    [SerializeField] private float checkRange = 1f;

    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;
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
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            checkAllDirections();
        }
        
    }

    private void checkAllDirections()
    {
        Debug.Log("CheckingAllDirections");
        RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, checkRange, pickupMask);
        RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, checkRange, pickupMask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, checkRange, pickupMask);
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, checkRange, pickupMask);

        RaycastHit2D[] hits = new RaycastHit2D[] {up, down, right, left};
        foreach (RaycastHit2D hit in hits){
            pickUpItem(hit);
        }
    }

    private void pickUpItem(RaycastHit2D hit)
    {
        if (hit)
        {
            Debug.Log("EvaluatingHit");
            if(itemHolding){

            }else
            {
                //Collider2D pickUpItem = Physics2D.OverlapCircle(hit.point, radius, pickupMask);
  
                itemHolding = hit.collider.gameObject;
                itemHolding.transform.position = holdSpot.position;
                itemHolding.transform.parent = transform;
                if(itemHolding.GetComponent<Rigidbody2D>()){
                    itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }
        
    }
}
