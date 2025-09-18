using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    [SerializeField] float speed;
    [SerializeField] int maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float friction;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;


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
        }

        if (Input.GetKey(rightKey))
        {
            moveX(speed);
        }
        if (Input.GetKey(upKey))
        {
            moveY(speed);
        }
        if (Input.GetKey(downKey))
        {
            moveY(-speed);
        }
    }

    void moveX(float velocity)
    {
        playerRB.linearVelocityX += velocity;
    }

    void moveY(float velocity)
    {
        playerRB.linearVelocityY += velocity; 
    }

    public KeyCode getLeftKey()
    {
        return leftKey;
    }

    public KeyCode getRightKey()
    {
        return rightKey;
    }

    public KeyCode getUpKey()
    {
        return upKey;
    }

    public KeyCode getDownKey()
    {
        return downKey;
    }

}



