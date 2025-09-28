using System;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    [SerializeField] private float checkRange;
    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;
    Animator animator;

    private GameObject itemHolding;
    private Vector3 direction;
    private WalkingDirection facingDirection;
    private PlayerMovement player;
    private Vector3 offset = new Vector3(0,0,0);
    [SerializeField] Vector3 eyeOffset;
    RaycastHit2D hit;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();
        eyeOffset = new Vector3(0, -.2f, 0);

        if (player == null)
        {
            Debug.LogError("PlayerMovement not found in parent!");
        }

    }
    void Update()
    {
        facingDirection = player.facingDirection;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (itemHolding)
            {
                putDownObject();
            }
            else { CheckDirectionFacing(); }
        }

        Debug.DrawRay(transform.position, Vector3.up*checkRange, Color.red);
        Debug.DrawRay(transform.position+eyeOffset, Vector3.left*checkRange, Color.red);
        Debug.DrawRay(transform.position+eyeOffset, Vector3.right*checkRange, Color.red);
        Debug.DrawRay(transform.position, Vector3.down*checkRange, Color.red);
    }

    private void checkAllDirections()
    {
        RaycastHit2D up = Physics2D.Raycast(transform.position, Vector3.up, checkRange, pickupMask);
        RaycastHit2D down = Physics2D.Raycast(transform.position, Vector3.down, checkRange, pickupMask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector3.right, checkRange, pickupMask);
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector3.left, checkRange, pickupMask);



        RaycastHit2D[] hits = new RaycastHit2D[] {up, down, right, left};
        foreach (RaycastHit2D hit in hits)
        {
            pickUpItem(hit);
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
                    hit = Physics2D.Raycast(transform.position-eyeOffset, Vector3.left, checkRange, pickupMask);
                    break;
                }
            case WalkingDirection.Right:
                {
                    hit = Physics2D.Raycast(transform.position-eyeOffset, Vector3.right, checkRange, pickupMask);
                    break;
                }
        }

        Debug.Log("Player facing direction is : " + facingDirection);

        if (hit != false) { pickUpItem(hit); }

    }

    private void pickUpItem(RaycastHit2D hit)
    {
        direction = hit.point;
        if (hit)
        {
            itemHolding = hit.collider.gameObject;

            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            
            if (itemHolding.GetComponent<BoxCollider2D>() != null) { itemHolding.GetComponent<BoxCollider2D>().enabled = false; }
            //Debug.Log("other items collider: " + itemHolding.GetComponent<Collider>().GetType().ToString());

            animator.SetTrigger("isHolding");
        }

    }

    private void putDownObject()
    {
        switch (facingDirection)
        {
            case WalkingDirection.Up:
                offset = new Vector3(0, .5f, 0);
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
        itemHolding.transform.position = new Vector3(Mathf.RoundToInt(itemHolding.transform.position.x)+.5f, Mathf.RoundToInt(itemHolding.transform.position.y)+.5f, Mathf.RoundToInt(itemHolding.transform.position.z));
        itemHolding.transform.parent = null;

        if (itemHolding.GetComponent<BoxCollider2D>() != null) { itemHolding.GetComponent<BoxCollider2D>().enabled = true; }

        itemHolding = null;
    }
}
