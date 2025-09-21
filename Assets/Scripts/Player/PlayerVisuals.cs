using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] Sprite[] idleSprites;
    [SerializeField] Sprite[] walking;
    [SerializeField] Sprite[] idleHolding;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float animationSpeed;
    [SerializeField] bool isAnimating;
    [SerializeField] bool isIdling;

    KeyCode leftKey;
    KeyCode rightKey;
    KeyCode upKey;
    KeyCode downKey;
    Coroutine _idle = null;
    Coroutine _walking = null;

    void Start()
    {
        rightKey = GetComponent<PlayerMovement>().getRightKey();
        leftKey = GetComponent<PlayerMovement>().getLeftKey();
        upKey = GetComponent<PlayerMovement>().getUpKey();
        downKey = GetComponent<PlayerMovement>().getDownKey();

        spriteRenderer = GetComponent<SpriteRenderer>();

        isAnimating = false;
        isIdling = false;
        
    }

    void Update()
    {
        if (!Input.anyKey)
        {
            if (_walking != null)
            {
                StopCoroutine(_walking);
                isAnimating = false;
            }

            if (!isIdling) { _idle = StartCoroutine(_idleSprites()); Debug.Log("starting idling anim"); }
        }

        else if (!isAnimating && Input.anyKey)
        {
            Debug.Log("in !isAnimating");

            if (Input.GetKey(rightKey))
            {
                StopIdle();
                spriteRenderer.flipX = false;
                _walking = StartCoroutine(_cycleSprites(walking));
            }

            if (Input.GetKey(leftKey))
            {
                StopIdle();
                spriteRenderer.flipX = true;
                _walking = StartCoroutine(_cycleSprites(walking));

            }
        }

    }

    void StopIdle() { if (_idle != null) { StopCoroutine(_idle); isIdling = false; } }

    IEnumerator _cycleSprites(Sprite[] sprites)
    {
        if (!Input.anyKey) { yield break; }

        isAnimating = true;
        float time = animationSpeed / sprites.Length;

        for (int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(time);
        }

        isAnimating = false;
    }

    IEnumerator _idleSprites()
    {
        isIdling = true;
        float time = animationSpeed / idleSprites.Length;

        for (int i = 0; i < idleSprites.Length; i++)
        {
            spriteRenderer.sprite = idleSprites[i];
            yield return new WaitForSeconds(time);
        }
        isIdling = false;
    }

}

