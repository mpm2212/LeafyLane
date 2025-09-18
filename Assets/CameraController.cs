using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float globalMaxX;
    [SerializeField] float globalMaxY;
    [SerializeField] float globalMinX;
    [SerializeField] float globalMinY;
    [SerializeField] float speed = 1f;

    void Start()
    {
        //Camera.main.orthographicSize = .5f;
    }

    void Update()
    {
        // code adapted from the video module on canvas
        Vector3 goal = target.transform.position + new Vector3(0, 0, -10);

        Vector3 newPosition = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);

        float maxY = globalMaxY - Camera.main.orthographicSize;
        float maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        float minY = globalMinY + Camera.main.orthographicSize;
        float minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;

    }

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0), new Vector3(globalMinX, globalMaxY, 0));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0), new Vector3(globalMaxX, globalMinY, 0));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMaxY, 0), new Vector3(globalMaxX, globalMinY, 0));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMaxY, 0), new Vector3(globalMaxX, globalMaxY, 0));

    }
}
