using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] float speed;
    [SerializeField] int maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float friction;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;

    public enum WalkingDirection
    {
        Up, 
        Down,
        Left, 
        Right
    };

    public WalkingDirection facingDir {get; private set;}


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerRB.linearVelocity = new Vector2(0, 0);
        Vector2 move = Vector2.zero;

        if (Input.GetKey(leftKey)) 
        { 
            move.x = -speed;
            facingDir = WalkingDirection.Left;
        }
        if (Input.GetKey(rightKey)) 
        { 
            move.x = speed;
            facingDir = WalkingDirection.Right;
        }
        if (Input.GetKey(upKey)) 
        { 
            move.y = speed;
            facingDir = WalkingDirection.Up;
        }
        if (Input.GetKey(downKey)) 
        { 
            move.y = -speed;
            facingDir = WalkingDirection.Down;
        }

        playerRB.linearVelocity = new Vector2(move.x, move.y) + playerRB.linearVelocity * 0.1f;
    }

    void moveX(float velocity) { playerRB.linearVelocityX += velocity; }

    void moveY(float velocity) { playerRB.linearVelocityY += velocity; }

    public KeyCode getLeftKey() { return leftKey; }

    public KeyCode getRightKey() { return rightKey; }

    public KeyCode getUpKey() { return upKey; }

    public KeyCode getDownKey() { return downKey; }

}



