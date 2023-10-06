using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(VehicleMovement))]
public class HandleInput : VehicleSystem
{
    public VehicleID ID;
    public Joystick moveJoystick;
    public Joystick shootJoystick;
    private VehicleMovement playerTankMovement;
    private VehicleAttack playerTankAttack;
    public CinemachineVirtualCamera cinemachine;
    public Transform viewMarker;
    [Range(0, 10)]
    public float lookAheadDistance;
    protected override void Awake()
    {
        base.Awake();
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        playerTankMovement = GetComponent<VehicleMovement>();
        playerTankAttack = GetComponentInChildren<VehicleAttack>();
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
        playerTankAttack.direction = shootJoystick.Direction; //Get shooting direction from joystick
        if (shootJoystick.Direction != Vector2.zero) //if player is holding the joystick then move the view marker to that direction
            viewMarker.position = Vector3.MoveTowards(viewMarker.position, transform.position + (Vector3)shootJoystick.Direction.normalized * lookAheadDistance, 0.5f);
        else // if not. move back to the tank
            viewMarker.position = transform.position;

    }
    public void Dash()
    {
        //cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0.2f;
        //cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.2f;
        vehicle.ID.events.OnDash?.Invoke(default);
        //StartCoroutine(ResetDamping());
    }



    public IEnumerator ResetDamping()
    {
        yield return new WaitForSeconds(playerTankMovement.dashingTime);
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1f;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.5f;
    }
}
