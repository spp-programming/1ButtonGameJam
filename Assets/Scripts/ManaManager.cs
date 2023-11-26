using System;
using Unity.Mathematics;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private float frameCycleTime = 10;
    private float randomOffset;

    public Sprite[] sprites;
    public float rotationSpeed;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        capsuleCollider.isTrigger = true;

        randomOffset = UnityEngine.Random.Range(0.0f, 10.0f);
    }

    void Update() {
        UpdateSprite();
    }

    void OnTriggerEnter2D (Collider2D coll)
	{
        if(coll.gameObject.name == "Player") {
            transform.parent.gameObject.GetComponent<LevelManager>().CollectEvent();       
            spriteRenderer.enabled = false;
            capsuleCollider.enabled = false;
        }
	}
    
    void UpdateSprite()
    {
        float animationProgress = (Time.realtimeSinceStartup + randomOffset) % frameCycleTime / frameCycleTime;
        int spriteIndex = (int)Mathf.Floor(animationProgress * sprites.Length);
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
