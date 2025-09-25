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
    Vector3 movement = new Vector3 (0, 0, 0);

    public enum WalkingDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    public WalkingDirection facingDirection {get; private set;}

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerRB.linearVelocity = new Vector2(0, 0);

        if (Input.GetKey(leftKey)) 
        { 
            moveX(-speed); 
            facingDirection = WalkingDirection.Left;}
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
    }

    void moveX(float velocity) { playerRB.linearVelocityX += velocity; }

    void moveY(float velocity) { playerRB.linearVelocityY += velocity; }

    public KeyCode getLeftKey() { return leftKey; }

    public KeyCode getRightKey() { return rightKey; }

    public KeyCode getUpKey() { return upKey; }

    public KeyCode getDownKey() { return downKey; }

}



