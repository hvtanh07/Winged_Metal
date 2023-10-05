using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public Transform target;
    public float flyingForce;
    public float turningTorque;
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void AssignTarget(Transform trackTarget){
        target = trackTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(transform.up * flyingForce, ForceMode2D.Force);//always flyforward

        Vector3 dir = target.position - transform.position; //get direction
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);

        rb.velocity = transform.up * flyingForce;


        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turningTorque * Time.fixedDeltaTime); //rotate to that direction

    }
}
