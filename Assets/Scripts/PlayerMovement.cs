using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator ani;

    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float directionX = 0;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpStrength = 8f;
    [SerializeField] private LayerMask jumpableSurface;

    private enum MovementState { idle, running, jumping, falling }
    // private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("PlayerMovement script loaded");
        ani = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (rb.bodyType != RigidbodyType2D.Static) {
            // Directional input
            directionX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

            // Jump input
            if (Input.GetButtonDown("Jump") && IsTouchingGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            }

            UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {

        MovementState state;
        // Set bools for animations
        if (directionX > 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // Jumping must have priority over movement and idling
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        ani.SetInteger("movementState", (int)state);
    }

    private bool IsTouchingGround()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, jumpableSurface);
    }

}
