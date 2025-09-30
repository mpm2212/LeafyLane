using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private float checkRange = 1.0f;
    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;

    Animator animator;

    private GameObject itemHolding;
    private GameObject item;
    private WalkingDirection facingDirection;
    private PlayerMovement player;
    private Vector3 offset = new Vector3(0,0,0);
    private HighlightObjects highlighter;

    private GameObject currentlyHighlighted;


    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();
        highlighter = GetComponent<HighlightObjects>();

        if (player == null)
        {
            Debug.LogError("PlayerMovement not found in parent!");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (itemHolding)
            {
                putDownObject();
            }
            else { CheckDirectionFacing(); }
        }

        facingDirection = player.facingDirection;
    }

    private void checkAllDirections()
    {
        if(currentlyHighlighted != null)
        {
            pickUpItem(currentlyHighlighted);
        }
    }
    
    void CheckDirectionFacing()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRange, pickupMask);

        if(hits.Length > 0){
            Collider2D closest = null;
            float closestDistance = Mathf.Infinity;
            foreach(Collider2D hit in hits){
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = hit;
                }
            }
            if(closest != null)
            {
                pickUpItem(closest.gameObject);
            }

        }

    }

    private void pickUpItem(GameObject item)
    {

        if (item != null)
        {
            itemHolding = item;
            Debug.Log("Item Holding: " + itemHolding.name);
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            if (itemHolding.GetComponent<Rigidbody2D>())
            {
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            }
            animator.SetBool("isHolding", true);
            highlighter.StopFlashing(itemHolding);
            //currentlyHighlighted = null;
        }

    }

    private void putDownObject()
    {

        Debug.Log("Putting Down Object");
        switch (facingDirection)
        {
            case WalkingDirection.Up:
                offset = new Vector3(0, 1, 0);
                break;
            case WalkingDirection.Down:
                offset = new Vector3(0, -1, 0);
                break;
            case WalkingDirection.Right:
                offset = new Vector3(1, 0, 0);
                break;
            case WalkingDirection.Left:
                offset = new Vector3(-1, 0, 0);
                break;
        }

        itemHolding.transform.position = transform.position + offset;
        itemHolding.transform.position = new Vector3(Mathf.RoundToInt(itemHolding.transform.position.x), Mathf.RoundToInt(itemHolding.transform.position.y), Mathf.RoundToInt(itemHolding.transform.position.z));
        itemHolding.transform.parent = null;
        if (itemHolding.GetComponent<Rigidbody2D>())
        {
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }
        itemHolding = null;
        animator.SetBool("isHolding", false);
        //Debug.Log("set bool isHolding to be " + animator.GetBool("isHolding"));

    }
}