using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    #region Serialize
    [SerializeField] Transform laserTransform;
    #endregion
    #region References
    private Vector3 position;
    private float width;
    private float height;
    private float timer;
    #endregion
    //hepsini böyle ayýr . private lara _ ekle sonuna.
    Rigidbody rb;
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
   
    private const string LASERTAG = "laser";

    //hepsini böyle tanýmla
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        timer = 0;
    }


    private void Update()
    {
        
        timer += Time.deltaTime;
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * speed, rb.velocity.y, mV * speed);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }

      

        if (Mathf.Abs(laserTransform.position.z - transform.position.z) < 1f && timer >= 1f)
        {
            UIManager.instance.AddScore();
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag(LASERTAG))
        {
            UIManager.instance.LoseGame();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="playerArea")
        {
            isGrounded = true;
        }
    }


}
