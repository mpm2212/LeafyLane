using UnityEngine;
using System.Collections;

public class HighlightObjects : MonoBehaviour
{

    private GameObject currentlyHighlighted;
    private Color originalColor;
    private Coroutine flashRoutine;
    private SpriteRenderer sr;
    int checkRange = 2;
    private GameObject item;
    [SerializeField] private Color flashColor = Color.blue;
    [SerializeField] private float flashSpeed = 0.5f;

    [SerializeField] LayerMask pickupMask;
    Collider2D[] nearbyItems;
    GameObject newHighlighted;

    void Start()
    {

    }

    void Update()
    {
        GetNearbyItems();
        HighlightNearbyItems();
    }

    void GetNearbyItems()
    {
        nearbyItems = Physics2D.OverlapCircleAll(transform.position, checkRange, pickupMask);
    }

    void CheckForClosestItem()
    {
        float closestDistance = Mathf.Infinity;

        foreach (var itemCollider in nearbyItems)
        {
            item = itemCollider.gameObject;

            float distance = Vector2.Distance(transform.position, item.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                newHighlighted = item;
            }
        }
    }

    private void HighlightNearbyItems()
    {
        //newHighlighted = null;

        CheckForClosestItem();

            if (currentlyHighlighted != null && currentlyHighlighted != newHighlighted)
            {
                //Resetting highlight
                StopFlashing(currentlyHighlighted);
                currentlyHighlighted = null;
            }

            if (newHighlighted != null && currentlyHighlighted != newHighlighted)
            {
                StartFlashing(newHighlighted);
                currentlyHighlighted = newHighlighted;
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
            sr.color = originalColor;
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
