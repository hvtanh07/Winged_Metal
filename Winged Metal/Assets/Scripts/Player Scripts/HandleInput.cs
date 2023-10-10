using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class HandleInput : VehicleSystem
{
    public VehicleID ID;
    public Joystick moveJoystick;
    public Joystick shootJoystick;
    public CinemachineVirtualCamera cinemachine;
    public Transform viewMarker;
    [Range(0, 10)]
    public float lookAheadDistance;
    protected override void Awake()
    {
        base.Awake();
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        moveJoystick = GameObject.FindGameObjectWithTag("Move joystick").GetComponent<Joystick>();
        shootJoystick = GameObject.FindGameObjectWithTag("Shoot joystick").GetComponent<Joystick>();
    }
    private void Update()
    {
        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;//Get keyboard input

        //MOVEMENT
        if (keyboardInput != Vector2.zero) //if there's keyboard input then use it
        {
            vehicle.ID.events.OnDirectionChange?.Invoke(keyboardInput);
            //playerTankMovement.direction = keyboardInput;
        }
        else //if not use the joystick
        {
            vehicle.ID.events.OnDirectionChange?.Invoke(moveJoystick.Direction);
            //playerTankMovement.direction = moveJoystick.Direction;
        }


        //CANON
        if (shootJoystick.Direction != Vector2.zero) //if player is holding the joystick then move the view marker to that direction
        {
            vehicle.ID.events.OnAttackDirectionChange?.Invoke(shootJoystick.Direction, true);
            viewMarker.position = Vector3.MoveTowards(viewMarker.position, transform.position + (Vector3)shootJoystick.Direction.normalized * lookAheadDistance, 0.5f);
        }
        else
        {
            viewMarker.position = transform.position;// if not. move back to the tank
            vehicle.ID.events.OnAttackDirectionChange?.Invoke(Vector2.zero, false);
        }


    }
    public void Dash()
    {
        vehicle.ID.events.OnDashCalled?.Invoke(default);
    }
}
