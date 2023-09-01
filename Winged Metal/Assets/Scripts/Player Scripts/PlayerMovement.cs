using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float enginePower;
    public float thursterForce;
    public float dashingTime;
    public float steeringTorque;
    public float weight;
    [Range(0.0f, 10.0f)]
    public float sideSlideAllowance;
    public Joystick joystick;
    public CinemachineVirtualCamera cinemachine;
    private Rigidbody2D rb;
    private bool dashing;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashing = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (dashing) return; // if player is dashing we won't do anything related to moving or turning

        rb.AddForce(joystick.Direction * enginePower / weight, ForceMode2D.Force); //move the tank

        if (joystick.Direction != Vector2.zero) // check moving and rotate to the moving direction
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, joystick.Direction);
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
    public void Dash()
    {
        //also check if there's still energy left
        if (dashing) return; //already dashing? nothing to do here
        StartCoroutine(DashToggle());
    }
    public IEnumerator DashToggle()
    {
        dashing = true;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0.2f;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.2f;
        if (joystick.Direction != Vector2.zero) // if player is holding joystick then dash with it
        {
            rb.AddForce(joystick.Direction.normalized * thursterForce / weight, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * thursterForce / weight, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(dashingTime);
        dashing = false;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1f;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1f;
    }
}
