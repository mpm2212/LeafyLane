using UnityEngine;

public class BobFollowPlayer : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float distance2player = 1.5f;
    [SerializeField] private float triggerDistance = 2.0f;
    [SerializeField] GameObject[] bobSpawnPoints;
    [SerializeField] private int pauseDuration = 5;

    private GameObject player;
    private bool follow = true;
    private bool canMove = true;
    private Animator animator;
    private Vector3 lakeCenter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if(follow){ player = GameObject.FindGameObjectWithTag("Player");} 
        chooseSpawnLocation();

    }

    // Update is called once per frame
    void Update()
    {

        if (!player) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(canMove)
        {
            if(distance < triggerDistance && distance > distance2player)
            { 
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

        }
    }

    void chooseSpawnLocation(){
        int position = Random.Range(1, bobSpawnPoints.Length);
        GameObject spawnPos = bobSpawnPoints[position];

    }

    //after you bring him back to the lake, it raises the event that the village has been unlocked
    //GameEvent.raise(Village)

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Lake"){
            GameEvents.RaiseRegionUnlocked("Village");
            canMove = false;
            Debug.Log("Bob is in Lake");
            animator.SetBool("inLake", true);
            Invoke(nameof(ResumeMovement), pauseDuration);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Lake")
        {
            animator.SetBool("inLake", false);
        }
    }

    void ResumeMovement()
    {
        canMove = true;
    }   
}
