using System;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    public float forceMultiplier = 10f; 
    public float liftDuration = 10f; 
    private float timer = 0f; 

    private Rigidbody rb;
    private bool isLifting = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isLifting)
        {
            isLifting = true;
            timer = 0f; 
        }
    }

    void FixedUpdate()
    {
        if (isLifting)
        {
            
            Vector3 upwardForce = -Physics.gravity * rb.mass * forceMultiplier;
            rb.AddForce(upwardForce);
            
            timer += Time.fixedDeltaTime;

            
            if (timer >= liftDuration)
            {
                isLifting = false;
            }
        }
        
    }
}
