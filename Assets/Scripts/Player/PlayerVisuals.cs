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
        if (player.facingDirection == PlayerMovement.WalkingDirection.Left) { spriteRenderer.flipX = true; walkingTrail.transform.localScale = new Vector3(-1, -1, -1); }
        else if (player.facingDirection == PlayerMovement.WalkingDirection.Right) { spriteRenderer.flipX = false; walkingTrail.transform.localScale = new Vector3(1, 1, 1); }
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
