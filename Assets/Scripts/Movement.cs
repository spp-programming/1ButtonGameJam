using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Rigidbody2D rb;

  public float jumpStrength = 2500;
  private bool jumped = false;
  public float speed = 4.0f; 

  public Sprite[] sprites = new Sprite[3];
  private SpriteRenderer spriteRenderer;

  // Time it takes to cycle all the frames in seconds
  public float frameCycleTime = 2;

  void Start()
  {
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

    if (Input.GetAxisRaw("Jump") != 0 && !jumped)
    {
      Debug.Log(Input.GetAxisRaw("Jump") == 1);
      rb.AddForce(new Vector2(0, jumpStrength));
      jumped = true;
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
    if((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0)) {
      transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    // If the player collides with a gmae object tagged "ground" let it jump again
    if(coll.gameObject.tag == "Ground") {
      jumped = false;
    }

    // Reverse the x speed if player collides witha wall and give it a push in the new direction
    if(coll.gameObject.tag == "Wall") {
      speed *= -1;
      rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }
  }
}