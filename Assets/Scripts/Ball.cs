using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float accelerationRate = 0.01f;
    private float maxVelocity = 3.0f;

    private void Awake()
    {
        // Loading difficulty settings from difficulty.json file
        if (DifficultyManager.Instance != null) 
        { 
            DifficultyManager.Instance.LoadDataFromFile();
            if (DifficultyManager.Instance.accelerationRate != 0.01f && DifficultyManager.Instance.maxVelocity != 3.0f)
            {
                accelerationRate = DifficultyManager.Instance.accelerationRate;
                maxVelocity = DifficultyManager.Instance.maxVelocity;
            }
        }

    }
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (m_Rigidbody != null)
        {
            var velocity = m_Rigidbody.velocity;
            //after a collision we accelerate a bit
            velocity += velocity.normalized * accelerationRate;

            //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
            if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
            {
                velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
            }

            //max velocity
            if (velocity.magnitude > maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }

            m_Rigidbody.velocity = velocity;
        }
    }

/*    public void SetMaxVelocity(float new_velocity) 
    {
        maxVelocity = new_velocity;
    }

    public void SetAccelerationRate(float new_acceleration) 
    {
        accelerationRate = new_acceleration;
    }*/
}
