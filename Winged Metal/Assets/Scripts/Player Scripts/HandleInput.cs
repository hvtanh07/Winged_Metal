using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(TankMovement))]
public class HandleInput : MonoBehaviour
{
    public Joystick moveJoystick;
    public Joystick shootJoystick;
    private TankMovement playerTank;
    private TankAttack playerTankAttack;
    public CinemachineVirtualCamera cinemachine;
    public Transform viewMarker;
    private void Awake()
    {
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        playerTank = GetComponent<TankMovement>();
        playerTankAttack = GetComponentInChildren<TankAttack>();
        moveJoystick = GameObject.FindGameObjectWithTag("Move joystick").GetComponent<Joystick>();
        shootJoystick = GameObject.FindGameObjectWithTag("Shoot joystick").GetComponent<Joystick>();
    }
    private void Update()
    {
        playerTank.direction = moveJoystick.Direction;
        playerTankAttack.direction = shootJoystick.Direction;
    }
    public void Dash()
    {
        if (playerTank.IsAbleToDash())
        {
            cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0.2f;
            cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.2f;
            StartCoroutine(playerTank.DashToggle());
            StartCoroutine(ResetDampingResetDamping());
        }

    }
    public IEnumerator ResetDampingResetDamping()
    {
        yield return new WaitForSeconds(playerTank.dashingTime);
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1f;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.5f;
    }
}
