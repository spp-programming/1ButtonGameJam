using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  public Rigidbody2D rb;
  public float jumpStrength = 5000;
  public bool jumped = false;
  public Vector2 velocity = new Vector2(2, 2);
  public Vector2 position = new Vector2(0, 0);

  public float speed = 16f; 

  void Start()
  {

  }

  void Update()
  {
    UpdateControls();
  }

  void UpdateControls()
  {
    // Enes (the goat's code)
    rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);

    if (Input.GetAxisRaw("Jump") != 0 && !jumped)
    {
      Debug.Log(Input.GetAxisRaw("Jump") == 1);
      rb.AddForce(new Vector2(0, jumpStrength));
      jumped = true;
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Ground") {
      jumped = false;
    }

    if(coll.gameObject.tag == "Wall") {
      speed *= -1;
    }
  }
}
