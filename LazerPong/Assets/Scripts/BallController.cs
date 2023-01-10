using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    #region Public
    public static BallController instance;
    public float startingForceSpeed;
    public int currentForceSpeed;
    #endregion
    #region References
    private Rigidbody rb_;
    
    private SphereCollider colliderOfBall_;
    #endregion

 
    //singleton
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
        //reach rigidbody component of ball.
        rb_ = GetComponent<Rigidbody>();
        //reach collider of ball.
        colliderOfBall_ = GetComponent<SphereCollider>();
    }
    void Start()
    {
        //in the begining we will add force to balls movement.
        AddForce();
    }

  

    public void AddForce()
    {
        //force direction will be random to seems natural.
        Vector3 direction = SetRandomDirectionForForce();
        rb_.AddForce(direction * startingForceSpeed, ForceMode.Impulse);
    }
    private Vector3 SetRandomDirectionForForce()
    {
        //take random numbers.
        float randomX = UnityEngine.Random.Range(-1, 1);
        float randomZ = UnityEngine.Random.Range(-1, 1);
        //we can also get zero while taking random number for x and z positions.And the ball can move straight between two paddles. I wrote condition bloks to prevent this.
        if (randomX >= 0)
        {
            randomX = 1;
        }
        else
        {
            randomX = -1;
        }

        if (Mathf.Abs(randomZ) < 0.1f)
        {
            randomZ = 1;
        }
        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        //random direction have to turn because we'r gonna use that while we adding force.
        return randomDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if ball touch the player or player area , ignore collision. (if we dont do that , ball direction or rotation will be changed and distorted)
        if (collision.gameObject.CompareTag(Constants.PLAYER_AREA_TAG))
        {
            IgnoreCollisionMethod(collision.gameObject);
        }

        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            IgnoreCollisionMethod(collision.gameObject);
        }

        
    }

    //I wrote Ignore Method to avoid writing same code twice.
    private void IgnoreCollisionMethod(GameObject anotherObject)
    {
        BoxCollider colliderOfOtherObject = anotherObject.GetComponent<BoxCollider>();
        Physics.IgnoreCollision(colliderOfOtherObject, colliderOfBall_);
    }

}
