using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private float checkRange;
    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;

    Animator animator;

    private GameObject itemHolding;
    private GameObject item;
    private WalkingDirection facingDirection;
    private PlayerMovement player;
    private Vector3 offset = new Vector3(0,0,0);

    private GameObject currentlyHighlighted;
    RaycastHit2D hit;


    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();

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
        switch (facingDirection)
        {
            case WalkingDirection.Up:
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.up, checkRange, pickupMask);
                    break;
                }
            case WalkingDirection.Down:
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.down, checkRange, pickupMask);
                    break;
                }
            case WalkingDirection.Left:
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.left, checkRange, pickupMask);
                    break;
                }
            case WalkingDirection.Right:
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.right, checkRange, pickupMask);
                    break;
                }
        }

        Debug.Log("Player facing direction is : " + facingDirection);

        if (hit != false) { pickUpItem(hit.collider.gameObject); }

    }

    private void pickUpItem(GameObject item)
    {
        //direction = hit.point;
        if (item != null)
        {
            itemHolding = item;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;

            BoxCollider2D itemCollider = itemHolding.GetComponent<BoxCollider2D>();
            if (itemCollider != null)
            {
                itemCollider.isTrigger = true;
            }
            animator.SetBool("isHolding", true);
            //StopFlashing(itemHolding);
            currentlyHighlighted = null;
        }

    }

    private void putDownObject()
    {
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
        itemHolding.transform.parent = null;

        BoxCollider2D itemCollider = itemHolding.GetComponent<BoxCollider2D>();
        if (itemCollider != null)
        {
            itemCollider.isTrigger = false;
        }
        itemHolding = null;
        animator.SetBool("isHolding", false);
        //Debug.Log("set bool isHolding to be " + animator.GetBool("isHolding"));

    }

 
}