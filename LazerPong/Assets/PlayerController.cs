using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Vector3 position;
    private float width;
    private float height;
    Rigidbody rb;
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }


    private void Update()
    {
        /* {
             Touch touch = Input.GetTouch(0);

             // Move the cube if the screen has the finger moving.
             if (touch.phase == TouchPhase.Moved)
             {
                 Vector2 pos = touch.position;
                 pos.x = (pos.x - width) / width;
                 pos.y = (pos.y - height) / height;
                 position = new Vector3(-pos.x, pos.y, 0.0f);

                 // Position the cube.
                 transform.position = position;
             }
         }*/
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * speed, rb.velocity.y, mV * speed);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag=="laser")
        {
            Debug.Log("died");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="platform")
        {
            isGrounded = true;
        }
    }


}
