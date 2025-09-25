using UnityEngine;

public class PickUpController : MonoBehaviour
{

    [SerializeField] private float checkRange = 1f;
    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;

    public float radius = 0.4f;
    private GameObject itemHolding;
    private Vector3 direction;
    private PlayerMovement.WalkingDirection facingDirection;
    private PlayerMovement player;
    private Vector3 offset = new Vector3(0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("PlayerMovement not found in parent!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (itemHolding)
            {
                putDownObject();
            }
            else { checkAllDirections(); }
        }
        facingDirection = player.facingDir;
        
        Debug.DrawLine(transform.position, Vector3.up, Color.red);
        Debug.DrawLine(transform.position, Vector3.down, Color.red);
        Debug.DrawLine(transform.position, Vector3.left, Color.red);
        Debug.DrawLine(transform.position, Vector3.right, Color.red);
    }

    private void checkAllDirections()
    {
        Debug.Log("CheckingAllDirections");
        RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, checkRange, pickupMask);
        RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, checkRange, pickupMask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, checkRange, pickupMask);
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, checkRange, pickupMask);



        RaycastHit2D[] hits = new RaycastHit2D[] {up, down, right, left};
        Debug.Log("hits: " + hits);
        foreach (RaycastHit2D hit in hits)
        {
            pickUpItem(hit);
            Debug.Log("ran pickUpItem: " + hit);
        }
    }

    private void pickUpItem(RaycastHit2D hit)
    {
        direction = hit.point;
        if (hit)
        {
            itemHolding = hit.collider.gameObject;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            if(itemHolding.GetComponent<Rigidbody2D>()){
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
        
    }

    private void putDownObject(){
        switch(facingDirection){
            case PlayerMovement.WalkingDirection.Up:
                offset = new Vector3(0,1,0);
                break;
            case PlayerMovement.WalkingDirection.Down: 
                offset = new Vector3(0,-1,0);
                break;
            case PlayerMovement.WalkingDirection.Right:
                offset = new Vector3(1,0,0);
                break;
            case PlayerMovement.WalkingDirection.Left:
                offset = new Vector3(-1,0,0);
                break;
        }
        
        itemHolding.transform.position = transform.position + offset; 
        itemHolding.transform.parent = null;
        if(itemHolding.GetComponent<Rigidbody2D>())
        {
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }
        itemHolding = null;
    }
}
