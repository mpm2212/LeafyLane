using UnityEngine;
using System.Collections;

public class HighlightObjects : MonoBehaviour
{

    private GameObject currentlyHighlighted;
    private Color originalColor;
    private Coroutine flashRoutine;
    private SpriteRenderer sr;
    private GameObject item;

    [SerializeField] private Color flashColor = Color.blue;
    [SerializeField] private float flashSpeed = 0.5f;
    [SerializeField] private int checkRange = 2;

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
        foreach(Collider2D itemCollider in nearbyItems ){
            item = itemCollider.gameObject;
            Debug.Log("Item: " + itemCollider.name);
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                newHighlighted = item;
            }
        }

        if(newHighlighted != currentlyHighlighted){
            if(currentlyHighlighted != null){StopFlashing(currentlyHighlighted); }
            if(newHighlighted != null) { StartFlashing(newHighlighted); }
            currentlyHighlighted = newHighlighted;
        }
            if(closestDistance > checkRange){
                Debug.Log("Closest Dist > 2");
                StopFlashing(currentlyHighlighted);
                currentlyHighlighted = null;
            }   
    }


    public void StartFlashing(GameObject item){
        sr = item.GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        flashRoutine = StartCoroutine(Flash(sr));
    }

    public void StopFlashing(GameObject item){
        if (item == null) return;
        sr = item.GetComponent<SpriteRenderer>(); 
        if(flashRoutine != null)
        {
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
