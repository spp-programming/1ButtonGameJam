using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCharging : MonoBehaviour
{

    private Rigidbody2D rb;

    public ParticleSystem chargingParticles;

    private bool isCharging = false;

    private int antiGravityForce = 100;


    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Movement.spellReady && !isCharging && !Movement.grounded)
        {
            StartCharging();
        } else if (!Input.GetButton("Fire1") && isCharging != false)
        {
            StopCharging();
        }
    }
    void StartCharging()
    {
        // Start the particle system
        chargingParticles.Play();

        rb.gravityScale = 0.4f;

        // Set the charging flag
        print("charging...");
        isCharging = true;
    }

    void StopCharging()
    {
        // Stop the particle system
        chargingParticles.Stop();

        // Add additional logic for when the charging is stopped
        print("charging stopped.");

        // Reset the charging flag
        rb.gravityScale = 1f;
        isCharging = false;
    }
}
