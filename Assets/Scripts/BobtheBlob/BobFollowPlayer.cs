using UnityEngine;

public class BobFollowPlayer : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float distance2player = 1.5f;
    [SerializeField] private float triggerDistance = 2.0f;
    [SerializeField] GameObject[] bobSpawnPoints;

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        chooseSpawnLocation();
    }

    // Update is called once per frame
    void Update()
    {

        if (!player) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < triggerDistance && distance > distance2player)
        { 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void chooseSpawnLocation(){
        int position = Random.Range(0, bobSpawnPoints.Length);
        GameObject spawnPos = bobSpawnPoints[position];

    }
}
