using UnityEngine;

public enum WalkingDirection { Up, Down, Left, Right };

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float drag = 5.0f;

    [Header("Input Keys")]
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;

    private Rigidbody2D playerRB;
    private Vector2 playerMovement;

    public WalkingDirection facingDirection { get; private set; }


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
        playerRB.linearVelocity = new Vector2(0, 0);
    
        if (Input.GetKey(leftKey))
        {
            moveX(-speed);
            facingDirection = WalkingDirection.Left;
        }
        if (Input.GetKey(rightKey))
        {
            moveX(speed);
            facingDirection = WalkingDirection.Right;
        }
        if (Input.GetKey(upKey))
        {
            moveY(speed);
            facingDirection = WalkingDirection.Up;
        }
        if (Input.GetKey(downKey))
        {
            moveY(-speed);
            facingDirection = WalkingDirection.Down;
        }

        NormalizeVelocity();
    }

    void moveX(float velocity) { playerRB.linearVelocityX += velocity; }

    void moveY(float velocity) { playerRB.linearVelocityY += velocity; }

    void NormalizeVelocity()
    {
        Vector2 playerMovement = playerRB.linearVelocity;

        if (playerMovement.magnitude > maxSpeed)
        {
            playerMovement.Normalize();
            playerRB.linearVelocity = playerMovement * maxSpeed;
        }
    }


}



