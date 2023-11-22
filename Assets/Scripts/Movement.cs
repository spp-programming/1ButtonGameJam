using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

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
    [Space(10)]
    public float speed;
    public BoxCollider2D rightCheck;

    public Sprite[] sprites = new Sprite[3];
    private SpriteRenderer spriteRenderer;
    private float localScaleX;

    // Time it takes to cycle all the frames in seconds
    public float frameCycleTime = 2;

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
    }

    void UpdateControls()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        //Check if groundCheck collider is triggered
        // Debug.Log(rb.velocity.y);
        if (groundCheck.IsTouchingLayers() && rb.velocity.y <= 0)
        {
            grounded = true;
        }

        //Check if rightCheck collider is triggered
        if (rightCheck.IsTouchingLayers())
        {
            speed *= -1;
            rb.AddForce(new Vector2(speed, rb.velocity.y), ForceMode2D.Impulse);
            UpdateSprite();
            // Using this line of code instead of old one for FirePoint to rotate as well
            transform.Rotate(0f, 180f, 0);
        }

        if (Input.GetButtonDown("Jump") && (grounded || coyoteTimeCounter > 0))
        {
            Jump();
            grounded = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutHeight);

        if (groundCheck.IsTouchingLayers())
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
    }

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
}