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
    private Vector3 offset = new Vector3(0, 0, 0);

    private GameObject currentlyHighlighted;
    RaycastHit2D hit;
    BoxCollider2D itemCollider;
    Rigidbody2D itemRB;


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

        // if (itemHolding)
        // {
        //     itemHolding.transform.position = holdSpot.position;
        // }

        facingDirection = player.facingDirection;
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

        if (hit != false) { pickUpItem(hit.collider.gameObject); }

    }

    private void pickUpItem(GameObject item)
    {
        //direction = hit.point;
        if (item != null)
        {
            itemHolding = item;
            itemRB = item.GetComponent<Rigidbody2D>();
            itemCollider = itemHolding.GetComponent<BoxCollider2D>();

            //itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;

            if (itemCollider != null)
            {
                itemCollider.enabled = false;
            }

            if (itemRB != null)
            {
                //itemRB.simulated = false;
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
        itemHolding.transform.position = new Vector3(Mathf.RoundToInt(itemHolding.transform.position.x), Mathf.RoundToInt(itemHolding.transform.position.y), Mathf.RoundToInt(itemHolding.transform.position.z));

        itemHolding.transform.parent = null;

        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
        if (itemRB != null)
        {
            itemRB.simulated = true;
        }
        itemHolding = null;
        itemRB = null;
        animator.SetBool("isHolding", false);
        //Debug.Log("set bool isHolding to be " + animator.GetBool("isHolding"));

    }


    void FixedUpdate()
    {
        if (itemRB != null)
        {
            itemRB.MovePosition(holdSpot.transform.position);

        }
    }
}