using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float frameCycleTime = 1;

    public Sprite[] sprites;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = transform.up * speed;
    }

    void Update()
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.layer) {
            case 6:
                GameObject.Destroy(this);
                break;
            case 8:
                // int id = coll.gameObject.GetComponent<SpellTriggerManager>();
                // LevelManager.Instance.SpellTrigger() 
                GameObject.Destroy(this);
                break;
        }
    }
}
