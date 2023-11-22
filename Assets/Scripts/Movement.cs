using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Rigidbody2D rb;

  public BoxCollider2D groundCheck;
  public float jumpStrength;
  private bool grounded = false;
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
    spriteRenderer= GetComponent<SpriteRenderer>();
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
    Debug.Log(rb.velocity.y);
    if(groundCheck.IsTouchingLayers() && rb.velocity.y <= 0) {
      grounded = true;
    }

    //Check if rightCheck collider is triggered
    if(rightCheck.IsTouchingLayers()) {
      speed *= -1;
      rb.AddForce(new Vector2(speed, rb.velocity.y), ForceMode2D.Impulse);
      UpdateSprite();
    }

    if (Input.GetAxisRaw("Jump") != 0 && grounded)
    {
      rb.AddForce(new Vector2(rb.velocity.x, jumpStrength), ForceMode2D.Impulse);
      grounded = false;
    }
  }

  void UpdateSprite() {
    // Cycles the index from 0 to the number of frames and repeats
    float animationProgress = Time.realtimeSinceStartup % frameCycleTime / frameCycleTime;
    int spriteIndex = (int) Mathf.Floor(animationProgress * sprites.Length);
    if (rb.velocity.sqrMagnitude != 0) {
      spriteRenderer.sprite = sprites[spriteIndex];
    }

    // Mirror the sprite on the y if its movement doesnt match it's sprite direction
    transform.localScale = new Vector3(localScaleX * Mathf.Sign(speed), transform.localScale.y, transform.localScale.z);
  }
}