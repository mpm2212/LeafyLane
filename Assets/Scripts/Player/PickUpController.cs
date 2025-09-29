using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private float checkRange;
    [SerializeField] public Transform holdSpot;
    [SerializeField] public LayerMask pickupMask;
    [SerializeField] private Color flashColor = Color.blue;
    [SerializeField] private float flashSpeed = 0.5f;
    Animator animator;

    public float radius = 0.4f;

    private GameObject itemHolding;
    private GameObject item;
    private Vector3 direction;
    private WalkingDirection facingDirection;
    private PlayerMovement player;
    private Vector3 offset = new Vector3(0,0,0);

    private GameObject currentlyHighlighted;
    private Color originalColor;
    private Coroutine flashRoutine;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();

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
        facingDirection = player.facingDirection;
        HighlightNearbyItems();
        
        // Debug.DrawLine(transform.position, Vector3.up + transform.position, Color.red);
        // Debug.DrawLine(transform.position, Vector3.down + transform.position, Color.red);
        // Debug.DrawLine(transform.position, Vector3.left + transform.position, Color.red);
        // Debug.DrawLine(transform.position, Vector3.right + transform.position, Color.red);
    }

    private void checkAllDirections()
    {
        Debug.Log("CheckingAllDirections");

        if(currentlyHighlighted != null)
        {
            Debug.Log("Currently Highlighted not null");
            pickUpItem(currentlyHighlighted);
        }
    }

    private void pickUpItem(GameObject item)
    {
        //direction = hit.point;
        if (item != null)
        {
            itemHolding = item;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            if (itemHolding.GetComponent<Rigidbody2D>())
            {
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            }
            animator.SetBool("isHolding", true);
            StopFlashing(itemHolding);
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
        if (itemHolding.GetComponent<Rigidbody2D>())
        {
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }
        itemHolding = null;
        animator.SetBool("isHolding", false);
        Debug.Log("set bool isHolding to be " + animator.GetBool("isHolding"));

    }

    private void HighlightNearbyItems()
    {
        Collider2D[] nearbyItems = Physics2D.OverlapCircleAll(transform.position, checkRange, pickupMask);
        GameObject newHighlighted = null;
        float closestDistance = Mathf.Infinity; 

        foreach (var itemCollider in nearbyItems)
        {
            item = itemCollider.gameObject;

            float distance = Vector2.Distance(transform.position, item.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                newHighlighted = item;
            }

            if(currentlyHighlighted != null && currentlyHighlighted != newHighlighted){
                //Resetting highlight
                StopFlashing(currentlyHighlighted);
                currentlyHighlighted = null;
            }

            if(newHighlighted != null && currentlyHighlighted != newHighlighted)
            {
                StartFlashing(newHighlighted);
                currentlyHighlighted = newHighlighted;
            }
        }
    }

    public void StartFlashing(GameObject item){
        sr = item.GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        flashRoutine = StartCoroutine(Flash(sr));
    }

    public void StopFlashing(GameObject item){
        sr = item.GetComponent<SpriteRenderer>();
        if(flashRoutine != null){
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        sr.color = originalColor;
    }

    private IEnumerator Flash(SpriteRenderer sr)
    {
        while(true)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);
            sr.color = originalColor;
            yield return new WaitForSeconds(flashSpeed);

        }
    }
}