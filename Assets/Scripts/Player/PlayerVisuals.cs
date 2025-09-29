using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    Animator animator;
    Rigidbody2D playerRB;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject walkingTrail;
    ParticleSystem walkingTrailSystem;
    private PlayerMovement player;
    WalkingDirection facingDirection;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkingTrailSystem = walkingTrail.GetComponent<ParticleSystem>();
        player = GetComponent<PlayerMovement>();


    }

    void Update()
    {
        facingDirection = player.GetWalkingDirection();

        animator.SetFloat("MovementMomentum", playerRB.linearVelocity.magnitude);

        FlipWalkingAnimation();
        if (playerRB.linearVelocity.magnitude == 0)
        {
            StopWalkingTrail();
        }
    }

    void FlipWalkingAnimation()
    {
        if (playerRB.linearVelocity.magnitude == 0) { return; }

        switch (facingDirection)
        {
            case WalkingDirection.Left:
                spriteRenderer.flipX = true;
                break;

            case WalkingDirection.Right:
                spriteRenderer.flipX = false;
                break;
        }

    }

    void PlayWalkingTrail()
    {
        walkingTrailSystem.Play();
    }

    void StopWalkingTrail()
    {
        walkingTrailSystem.Stop();
    }
    
}
