using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private int id;
    public float rotationSpeed;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        capsuleCollider.isTrigger = true;
    }

    void Update() {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }

    public void SetID(int _id) {
        id = _id;
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
