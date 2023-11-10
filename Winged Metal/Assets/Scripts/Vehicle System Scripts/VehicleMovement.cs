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
    [HideInInspector]
    public Vector2 direction;
    private Rigidbody2D rb;
    private bool dashing;
    private int enConsum;
    bool ableToDash;
    public bool isShooting;
    void OnEnable()
    {
        vehicle.events.OnDirectionChange += ChangeDirection;
        vehicle.events.OnDashCalled += Dash;
        vehicle.events.OnEnUpdate += UpdateAmountEn;
        vehicle.events.OnAttackDirectionChange += UpdateAttack;
    }

    private void ChangeDirection(Vector2 newdirection)
    {
        direction = newdirection;
    }
    private void UpdateAttack(Vector2 direction,bool shooting)
    {
        isShooting = shooting && (direction != Vector2.zero);
    }
    private void UpdateAmountEn(float currentEn)
    {
        ableToDash = currentEn >= enConsum;
    }

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        //rb = GetComponent<Rigidbody2D>();
        rb = transform.root.GetComponent<Rigidbody2D>();
        dashing = false;
        ableToDash = true;
        enConsum = weight * 3; //if there's modify stat function then move this to that function
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (dashing) return; // if player is dashing we won't do anything related to moving or turning
        float finalEnginePower = enginePower * (isShooting ? 0.8f:1f);

        rb.AddForce(direction * finalEnginePower / weight, ForceMode2D.Force); //move the tank

        if (direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, steeringTorque * Time.fixedDeltaTime);
        }
    }


    public void Dash(Vector2 dashDirection = default)
    {
        if (dashing) return; //already dashing? nothing to do here
        if (ableToDash)
        {
            StartCoroutine(DashToggle(dashDirection));
        }
    }
    public IEnumerator DashToggle(Vector2 dashDirection = default)
    {
        vehicle.events.OnEnUsed?.Invoke(enConsum);
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
        vehicle.events.OnDashComplete?.Invoke();
    }
}
