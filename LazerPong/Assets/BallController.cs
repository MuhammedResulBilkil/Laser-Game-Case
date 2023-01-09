using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce;
    SphereCollider collider;
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerArea")
        {
            IgnoreCollision(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            IgnoreCollision(collision.gameObject);
        }
    }

    void IgnoreCollision(GameObject anotherObject)
    {
        BoxCollider colliderOfOtherObject = anotherObject.GetComponent<BoxCollider>();
        Physics.IgnoreCollision(colliderOfOtherObject, collider);
    }

}
