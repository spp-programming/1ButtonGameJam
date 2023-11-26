using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private bool hasTransitioned = false;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Open() {
        spriteRenderer.enabled = true;
        capsuleCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && !hasTransitioned)
        {
            LevelManager.Instance.LoadNextLevel(LevelManager.Instance.currentLevel + 1);
            hasTransitioned = true;
        }
    }
}
