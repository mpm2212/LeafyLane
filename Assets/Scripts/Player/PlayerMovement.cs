using UnityEngine;

public enum WalkingDirection { Up, Down, Left, Right };

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int maxSpeed;
    [SerializeField] float drag = 5.0f;

    [Header("Input Keys")]
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;

    private Rigidbody2D playerRB;
    private Vector2 playerMovement;

    public WalkingDirection facingDirection { get; private set;}


    public WalkingDirection GetWalkingDirection()
    {
        return facingDirection;
    }


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        playerRB.linearDamping = drag;
    }

    void Update()
    {
        playerMovement = Vector2.zero;
        if (Input.GetKey(leftKey))
        {
            playerMovement.x = -1.0f;
            facingDirection = WalkingDirection.Left;
        }
        if (Input.GetKey(rightKey))
        {
            playerMovement.x = 1.0f;
            facingDirection = WalkingDirection.Right;
        }
        if (Input.GetKey(upKey))
        {
            playerMovement.y = 1.0f;
            facingDirection = WalkingDirection.Up;
        }
        if (Input.GetKey(downKey))
        {
            playerMovement.y = -1.0f;
            facingDirection = WalkingDirection.Down;
        }

        playerMovement.Normalize();
    }

    void FixedUpdate(){

        if (playerMovement != Vector2.zero)
        {
            Vector2 force = playerMovement * speed;
            playerRB.AddForce(force);

            if (playerRB.linearVelocity.magnitude > maxSpeed)
            {
                playerRB.linearVelocity = playerRB.linearVelocity.normalized * maxSpeed;
            }
        }
    }
}



