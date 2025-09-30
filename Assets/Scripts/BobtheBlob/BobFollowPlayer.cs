using UnityEngine;

public class BobFollowPlayer : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float distance2player = 1.5f;
    [SerializeField] private float triggerDistance = 2.0f;
    [SerializeField] GameObject[] bobSpawnPoints;
    [SerializeField] private int pauseDuration = 5;
    [SerializeField] private float rescueTime = 30f;

    private float timer;
    private bool isRescueActive = false;
    private bool MissionComplete = false;

    private GameObject player;
    private bool canMove = true;
    private Animator animator;
    private Vector3 lakeCenter;
    Rigidbody2D bobRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chooseSpawnLocation();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bobRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < triggerDistance && distance > distance2player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            //bobRB.MoveTowards
        }

        if(isRescueActive && MissionComplete == false){
            timer -= Time.deltaTime;
            if(timer <= 0){
                RescueFailed();
            }
        }
    }

    void chooseSpawnLocation(){
        int index = Random.Range(0, bobSpawnPoints.Length);
        GameObject spawnPos = bobSpawnPoints[index];

        if (spawnPos != null)
        {
            transform.position = spawnPos.transform.position;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isRescueActive){
            //Debug.Log("Bob has been found! start the timer");
            timer = rescueTime;
            isRescueActive = true;
        }

        if(other.gameObject.tag == "Lake")
        {
            EnteredLake();
            
        }
    }

    public void EnteredLake()
    {
        //Debug.Log("entered lake");
        canMove = false;
        animator.SetBool("inLake", true);
        Invoke(nameof(ResumeMovement), pauseDuration);

        RescueCompleted();
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

    void RescueCompleted()
    {
        //Debug.Log("Bob has been brought back");
        MissionComplete = true;
        isRescueActive = false;
    }

    void RescueFailed(){
        //Debug.Log("You failed");
        isRescueActive = false;
        chooseSpawnLocation();
    }
}
