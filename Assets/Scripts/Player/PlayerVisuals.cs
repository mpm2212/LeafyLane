using Unity.Mathematics;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    Animator animator;
    Rigidbody2D playerRB;
    SpriteRenderer spriteRenderer;
    float intx;
    [SerializeField] bool isHolding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetFloat("MovementMomentum", playerRB.linearVelocity.magnitude);
        animator.SetBool("isHolding", isHolding);

        FlipWalkingAnimation();
    }

    void FlipWalkingAnimation()
    {
        if (playerRB.linearVelocityX == 0) { return; }
        if (playerRB.linearVelocityX < 0.01) { spriteRenderer.flipX = true; }
        else if (playerRB.linearVelocityX > 0.01) { spriteRenderer.flipX = false; }
    }
}
