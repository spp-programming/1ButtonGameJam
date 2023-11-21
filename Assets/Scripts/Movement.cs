using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Rigidbody2D rb;

  public float jumpStrength = 5000;
  private bool jumped = false;
  public Sprite[] sprites = new Sprite[3];
  private SpriteRenderer spriteRenderer;

  private float startTime;
  public float speed = 16f; 

  public float TIME_FOR_ALL_FRAMES;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer= GetComponent<SpriteRenderer>();

    startTime = Time.realtimeSinceStartup;
  }

  void Update()
  {
    UpdateControls();
    UpdateSprite();
  }

  void UpdateControls()
  {
    // rb.velocity = new Vector2(speed, rb.velocity.y);

    if (Input.GetAxisRaw("Jump") != 0 && !jumped)
    {
      Debug.Log(Input.GetAxisRaw("Jump") == 1);
      rb.AddForce(new Vector2(0, jumpStrength));
      jumped = true;
    }
  }

  void UpdateSprite() {
    int spriteIndex = (int) Mathf.Floor(((Time.realtimeSinceStartup % TIME_FOR_ALL_FRAMES) / TIME_FOR_ALL_FRAMES * 3));

    if (rb.velocity.sqrMagnitude == 0) {
      spriteRenderer.sprite = sprites[spriteIndex];
    } else if(rb.velocity.sqrMagnitude != 0) {

    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Ground") {
      jumped = false;
    }

    if(coll.gameObject.tag == "Wall") {
      speed *= -1;
      rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }
  }
}
