using System.Collections;
using TMPro;
using UnityEngine;

public class BobFollowPlayer : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float distance2player = 1.5f;
    [SerializeField] private float triggerDistance = 2.0f;
    [SerializeField] GameObject[] bobSpawnPoints;
    [SerializeField] private int pauseDuration = 5;
    [SerializeField] private float rescueTimer = 30f;

    private float timer;
    private bool isRescueActive = false;
    private bool missionComplete = false;

    bool villageTriggered;

    private GameObject player;
    private bool canMove = true;
    private Animator animator;
    private Vector3 lakeCenter;
    [SerializeField] GameObject bobTimerText;
    TextMeshProUGUI timerText;
    Rigidbody2D bobRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chooseSpawnLocation();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bobRB = GetComponent<Rigidbody2D>();
        villageTriggered = false;
        if (bobTimerText != null){ timerText = bobTimerText.GetComponent<TextMeshProUGUI>(); bobTimerText.SetActive(false); }

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

        if (isRescueActive && missionComplete == false)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time left: " + (int)timer;
            if (timer <= 0)
            {
                RescueFailed();
            }
        }
        if (missionComplete && !villageTriggered && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            GameEvents.RaiseRegionUnlocked("Village");
            villageTriggered = true;
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
        if (other.CompareTag("Player") && !isRescueActive && !missionComplete)
        {
            //Debug.Log("Bob has been found! start the timer");
            timer = rescueTimer;
            isRescueActive = true;
            bobTimerText.SetActive(true);
        }

        if(other.gameObject.tag == "Lake" && !missionComplete)
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
        missionComplete = true;
        isRescueActive = false;
        bobTimerText.SetActive(false);
        GameEvents.RaiseFoundBobEvent(this.gameObject);
    }

    void RescueFailed()
    {
        isRescueActive = false;
        chooseSpawnLocation();
        timer = rescueTimer;
        GameEvents.RaiseDidntFindBobEvent(this.gameObject);
    }
}
