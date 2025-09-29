using UnityEngine;
using System.Collections;

public class HighlightObjects : MonoBehaviour
{

    private GameObject currentlyHighlighted;
    private Color originalColor;
    private Coroutine flashRoutine;
    private SpriteRenderer sr;
    int checkRange = 3;
    private GameObject item;
    [SerializeField] private Color flashColor = Color.blue;
    [SerializeField] private float flashSpeed = 0.5f;

    [SerializeField] LayerMask pickupMask;

    void Start()
    {

    }

    void Update()
    {
        HighlightNearbyItems();
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

            if (distance < closestDistance)
            {
                closestDistance = distance;
                newHighlighted = item;
            }

            if ((currentlyHighlighted != null && currentlyHighlighted != newHighlighted) || closestDistance > 2f)
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
