using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private bool hasTransitioned = false;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private float frameCycleTime = 1.0f;
    public Sprite[] sprites;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update() {
        UpdateSprite();
    }

    public void Open() {
        spriteRenderer.enabled = true;
        capsuleCollider.enabled = true;
    }

    void UpdateSprite()
    {
        // Cycles the index from 0 to the number of frames and repeats
        float animationProgress = Time.realtimeSinceStartup % frameCycleTime / frameCycleTime;
        int spriteIndex = (int)Mathf.Floor(animationProgress * sprites.Length);
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && !hasTransitioned && spriteRenderer.enabled)
        {
            LevelManager.Instance.LevelCompleteEvent();
            LevelManager.Instance.LoadNextLevel(LevelManager.Instance.currentLevel + 1);
            hasTransitioned = true;
        }
    }
}
