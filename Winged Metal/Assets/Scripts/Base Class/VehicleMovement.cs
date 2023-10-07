using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using System;

public class VehicleMovement : VehicleSystem
{
    public float enginePower;
    public float thursterForce;
    public float dashingTime;
    public float steeringTorque;
    public int weight;
    [Range(0.0f, 10.0f)]
    public float sideSlideAllowance;
    [HideInInspector]
    public Vector2 direction;
    private Rigidbody2D rb;
    private bool dashing;
    private int enConsum;
    void OnEnable()
    {
        vehicle.ID.events.OnMovementDirectionChange += ChangeDirection;
        vehicle.ID.events.OnDash += Dash;
    }

    private void ChangeDirection(Vector2 newdirection)
    {
        direction = newdirection;
    }

    public void InitiateParameter(float EnginePower, float ThursterForce, int TurretWeight, int SecWeponWeight, int BodyWeight, int AutoGunWeight, int EngineWeight, int GeneratorWeight, int RepairKitWeight)
    {
        enginePower = EnginePower;
        thursterForce = ThursterForce;
        weight = TurretWeight + SecWeponWeight + BodyWeight + AutoGunWeight + EngineWeight + GeneratorWeight + RepairKitWeight;
        enConsum = weight;
    }

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        dashing = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (dashing) return; // if player is dashing we won't do anything related to moving or turning

        rb.AddForce(direction * enginePower / weight, ForceMode2D.Force); //move the tank

        if (direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, steeringTorque * Time.fixedDeltaTime);
        }
        else if (rb.velocity.magnitude > 0) //detect if object is side slide
        {
            Vector2 localVelocity = rb.transform.InverseTransformDirection(rb.velocity);
            if (Mathf.Abs(localVelocity.x) > sideSlideAllowance)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }




    public void Dash(Vector2 dashDirection = default)
    {
        if (dashing) return; //already dashing? nothing to do here
        if (vehicle.currentEnergy < weight) return;
        StartCoroutine(DashToggle(dashDirection));
    }
    public IEnumerator DashToggle(Vector2 dashDirection = default)
    {
        vehicle.ConsumeEnergy(weight);
        dashing = true;
        if (dashDirection != default)//if npc have target direction to dash then dash on that direction
        {
            rb.AddForce(dashDirection.normalized * thursterForce / weight, ForceMode2D.Impulse);
        }
        else if (direction != Vector2.zero) // if player is holding joystick then dash with it
        {
            rb.AddForce(direction.normalized * thursterForce / weight, ForceMode2D.Impulse);
        }
        else //if there's no target direction then just dash forward
        {
            rb.AddForce(transform.up * thursterForce / weight, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(dashingTime);
        dashing = false;
    }
}