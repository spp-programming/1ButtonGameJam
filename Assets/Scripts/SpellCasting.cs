using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBallPrefab;
    public int shootCounter = 0;

    // Update is called once per frame
    void Update()
    {
        // Confirm that we can shoot
        if (Input.GetButtonUp("Fire1") && !Movement.grounded && shootCounter == 0)
        {
            Shoot();
            shootCounter += 1;
        }

        // Reset the counter
        if (Movement.grounded)
        {
            shootCounter = 0;
        }
    }

    void Shoot()
    {
        // Shooting Logic
        Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
        
    }
}
