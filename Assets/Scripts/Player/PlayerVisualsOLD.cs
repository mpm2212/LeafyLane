using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerVisualsOLD : MonoBehaviour
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
    KeyCode lastKey;
    Coroutine _idle;
    Coroutine _walking;

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

            if (!isIdling) { _idle = StartCoroutine(_idleSprites()); }
        }

        else if (!isAnimating)
        {
            if (Input.GetKey(rightKey))
            {
                StopIdle();
                StopWalking(lastKey, rightKey);
                spriteRenderer.flipX = false;
                lastKey = rightKey;
                _walking = StartCoroutine(_cycleSprites(walking));
            }

            if (Input.GetKey(leftKey))
            {
                StopIdle();
                StopWalking(lastKey, leftKey);
                spriteRenderer.flipX = true;
                lastKey = leftKey;
                _walking = StartCoroutine(_cycleSprites(walking));
                

            }
        }

    }

    void StopIdle() { if (_idle != null) { StopCoroutine(_idle); isIdling = false; } }
    void StopWalking(KeyCode lastKey, KeyCode currentKey){ if (lastKey != currentKey && _walking != null) { StopCoroutine(_walking); isAnimating = false; Debug.Log("stopped walking"); }}

    IEnumerator _cycleSprites(Sprite[] sprites)
    {
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

