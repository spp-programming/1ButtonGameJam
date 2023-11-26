using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBallPrefab;
    public int shootCounter = 0;

    // Boolean flag to check if the fire button is held
    private bool isFireButtonDown = false;

    // Update is called once per frame
    void Update()
    {
        // Confirm that we can shoot
        if (Input.GetButtonUp("Fire1") && Movement.spellReady && shootCounter == 0)
        {
            shootCounter += 1;
            Shoot();
        }

        // Check if the fire button is held

        // Reset the counter
        if (Movement.grounded) {
            shootCounter = 0;
        }
    }

    void Shoot() {
        // Shooting Logic with a specified fireball size
        GameObject newFireball = Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }
}
