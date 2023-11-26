using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public Sprite[] sprites = new Sprite[2];
    private SpriteRenderer spriteRenderer;
    private float frameCycleTime = 1.0f;

    public LayerMask groundLayer;
    public Transform ObstacleCheck;
    private bool isObstructed = false;
    private float speed = 2.0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateControls();
        UpdateSprite();
    }

    public void Die()
    {
        LevelManager.Instance.CreateMana(new Vector3(transform.position.x, transform.position.y, 0));
        Destroy(gameObject);
    }

    void UpdateControls()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        isObstructed = Physics2D.OverlapCircle(ObstacleCheck.position, 0.1f, groundLayer);
        
        if (isObstructed)
        {
            speed *= -1;
            rb.AddForce(new Vector2(speed, rb.velocity.y), ForceMode2D.Impulse);
            UpdateSprite();
            // Using this line of code instead of old one for FirePoint to rotate as well
            transform.Rotate(0f, 180f, 0);
        }
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
    }
}