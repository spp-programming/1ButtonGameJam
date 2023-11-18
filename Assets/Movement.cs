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

  void Start()
  {

  }

  void Update()
  {
    UpdateControls();
  }

  void UpdateControls()
  {
    if (Input.GetAxisRaw("Jump") != 0 && jumped == false)
    {
      Debug.Log(Input.GetAxisRaw("Jump") == 1);
      rb.AddForce(Vector2.up * jumpStrength);
      jumped = true;
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    jumped = false;
  }
}
