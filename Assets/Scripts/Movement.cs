using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    public LayerMask groundLayer;

    public BoxCollider2D groundCheck;

    [Header("Jump Mechanics")][Range(1, 50)]
    public float jumpStrength;
    [Range(0.5f, 1)]
    public float jumpCutHeight;

    [Range(0.01f, 0.5f)]
    public float CoyoteTime = 0.1f;
    private float coyoteTimeCounter;
    bool isCoyoteTimeOver;

    // Static and Public because it is needed for the FireBall.cs 
    static public bool grounded = false;
    static public bool spellReady = false;
    [Space(10)]
    public float speed;

    public BoxCollider2D rightCheck;

    public Sprite[] sprites = new Sprite[3];
    private SpriteRenderer spriteRenderer;
    private float localScaleX;

    // Time it takes to cycle all the frames in seconds
    public float frameCycleTime = 2;

    [Space(10)] public Transform ObstacleCheck;
    private bool isObstructed;

    void Start()
    {
        localScaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateControls();
        UpdateSprite();

        IsGrounded();
    }

    void UpdateControls()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        //Check if groundCheck collider is triggered
        // Debug.Log(rb.velocity.y);
        if (IsGrounded() && rb.velocity.y <= 0)
        {
            grounded = true;
            spellReady = false;
        }

        isObstructed = Physics2D.OverlapCircle(ObstacleCheck.position, 0.1f, groundLayer);

        //Check if rightCheck collider is triggered
        if (isObstructed)
        {
            speed *= -1;
            rb.AddForce(new Vector2(speed, rb.velocity.y), ForceMode2D.Impulse);
            UpdateSprite();
            // Using this line of code instead of old one for FirePoint to rotate as well
            transform.Rotate(0f, 180f, 0);
        }

        if (Input.GetButtonDown("Jump") && (IsGrounded() || coyoteTimeCounter > 0))
        {
            Jump();
            grounded = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutHeight);
            spellReady = true;
        }

        if (IsGrounded() && rb.velocity.y <= 0)
        {
            coyoteTimeCounter = CoyoteTime;
            isCoyoteTimeOver = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (coyoteTimeCounter <= 0 && !isCoyoteTimeOver)
                isCoyoteTimeOver = true;
        }

        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -16.0f, 16.0f), 0);
    }

    private void OnDrawGizmos() => Gizmos.DrawWireSphere(ObstacleCheck.position, 0.1f);

    void Jump(float velocity = 1)
    {
        rb.velocity += Vector2.up * (jumpStrength * velocity);
        coyoteTimeCounter = 0;
    }

    void UpdateSprite()
    {
        // Cycles the index from 0 to the number of frames and repeats
        float animationProgress = Time.realtimeSinceStartup % frameCycleTime / frameCycleTime;
        int spriteIndex = (int)Mathf.Floor(animationProgress * sprites.Length);
        if (rb.velocity.sqrMagnitude != 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }

        // Mirror the sprite on the y if its movement doesnt match it's sprite direction
        // transform.localScale = new Vector3(localScaleX * Mathf.Sign(speed), transform.localScale.y, transform.localScale.z);
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.25f;
        RaycastHit2D hit2D = Physics2D.BoxCast(groundCheck.bounds.center, groundCheck.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeight, groundLayer);
        Color rayColor = hit2D.collider != null ? Color.green : Color.red;

        // Optional, Debug.DrawRay code can be deleted later
        Debug.DrawRay(groundCheck.bounds.center + new Vector3(groundCheck.bounds.extents.x, 0), Vector2.down * (groundCheck.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(groundCheck.bounds.center - new Vector3(groundCheck.bounds.extents.x, 0), Vector2.down * (groundCheck.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(groundCheck.bounds.center - new Vector3(groundCheck.bounds.extents.x, groundCheck.bounds.extents.y + extraHeight), groundCheck.bounds.extents.x * 2 * Vector2.right, rayColor);

        return hit2D.collider != null;
    }
}