using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce;
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        AddStartingForce();
    }

  

    private void AddStartingForce()
    {

        float randomX = UnityEngine.Random.Range(-1, 1);
        float randomZ = UnityEngine.Random.Range(-1, 1);
        if (randomX >= 0)
        {
            randomX = 1;
        }
        else
        {
            randomX = -1;
        }

        if(Mathf.Abs(randomZ) <0.1f)
        {
            randomZ = 1;
        }
        Vector3 randomDirection = new Vector3(randomX,0, randomZ).normalized;

        rb.AddForce(randomDirection * bounceForce, ForceMode.Impulse);
    }

   


}
