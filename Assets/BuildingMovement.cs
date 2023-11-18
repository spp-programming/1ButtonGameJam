using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    public Transform transform;
    public SpriteRenderer spriteRenderer;
    public Vector3 velocity;
    public float distance;
    public Sprite[] sprites = new Sprite[2];

    void Start()
    {
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        distance = UnityEngine.Random.Range(0.50f, 1.25f);
        spriteRenderer.sortingOrder = (int) Math.Floor(distance * 100);
        velocity *= distance;
        transform.localScale *= distance;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime, 0, 0);
    }
}
