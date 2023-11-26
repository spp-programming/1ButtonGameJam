using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    public float rotationSpeed;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        capsuleCollider.isTrigger = true;
    }

    void Update() {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D (Collider2D coll)
	{
        if(coll.gameObject.name == "Player") {
            transform.parent.gameObject.GetComponent<LevelManager>().CollectEvent();       
            spriteRenderer.enabled = false;
            capsuleCollider.enabled = false;
        }
	}
}
