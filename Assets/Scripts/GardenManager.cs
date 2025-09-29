using System.Collections;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager Instance { get; private set; }

    int meadowItemCount = 0;
    int meadow2ItemCount = 0;
    int lakeItemCount = 0;
    int villageItemCount = 0;
    int forestItemCount = 0;


    void Awake()
    {
        GardenManagerSingleton();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void GardenManagerSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.tag;
        Debug.Log("other tag: " + otherTag);

        switch (otherTag)
        {
            case ("Meadow"):
                meadowItemCount++;
                Debug.Log("meadow item count: " + meadowItemCount);
                return;

            case ("Meadow-2"):
                meadow2ItemCount++;
                return;

            case ("Village"):
                villageItemCount++;
                return;

            case ("Lake"):
                lakeItemCount++;
                return;

            case ("Forest"):
                forestItemCount++;
                return;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        string otherTag = other.tag;
        if (otherTag == "Meadow")
        {
            meadowItemCount--;
            Debug.Log("meadow item count: " + meadowItemCount);
        }
    }


}
