using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedTrackingExplosive : MonoBehaviour
{
    Vector3 target;
    Vector3 dir;
    public float flyingForce;
    public bool startTracking;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void AssignTarget(Vector3 trackTarget)
    {
        target = trackTarget;
        StartCoroutine(StartTrackingWithDelay());
    }
    IEnumerator StartTrackingWithDelay()
    {
        rb.velocity = transform.up * flyingForce * 5;
        yield return new WaitForSeconds(0.3f);
        dir = (target - transform.position).normalized;
        rb.velocity = Vector2.zero;
        startTracking = true;
    }

    void FixedUpdate()
    {
        if (startTracking)
        {
            rb.AddForce(dir * flyingForce, ForceMode2D.Impulse);
        }
    }

    void OnDisable()
    {
        startTracking = false;
    }
}
