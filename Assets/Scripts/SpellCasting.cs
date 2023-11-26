using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBallPrefab;
    public int shootCounter = 0;

    public float sizeIncreaseRate = 0.5f;

    public float maxFireballSize = 5.0f;

    // Boolean flag to check if the fire button is held
    private bool isFireButtonDown = false;

    // Update is called once per frame
    void Update()
    {
        // Confirm that we can shoot
        if (Input.GetButtonDown("Fire1") && Movement.spellReady && shootCounter == 0)
        {
            shootCounter += 1;
        }

        // Check if the fire button is held

        // Reset the counter
        if (Movement.grounded) {
            shootCounter = 0;
        }
    }

    void Shoot(float fireballSize) {
        // Shooting Logic with a specified fireball size
        GameObject newFireball = Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }
}
