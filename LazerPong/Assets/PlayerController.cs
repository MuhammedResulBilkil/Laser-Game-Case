using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float speed;
    public float jumpForce;
    #region Serialize
    [SerializeField] Transform laserTransform;
    
    #endregion
    #region References
    //private float firstYPosition_;
    private float timer_;
    private Rigidbody rb_;
   
    private bool isGrounded = false;
    #endregion

   

    public delegate void PlayerActions();
    public PlayerActions playerActions;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb_ = GetComponent<Rigidbody>();
     
        //firstYPosition_ = transform.position.y;
        timer_ = 0;
    }


    private void Update()
    {

       PlayerActionsMethod();


    }
    public void PlayerActionsMethod()
    {
        if(playerActions!=null)
        {
            playerActions();
        }
       
    }

    private void OnEnable()
    {
        AddMethodToDelegate(PlayerMovement);
        AddMethodToDelegate(PlayerJump);
        AddMethodToDelegate(isPlayerJumpOverTheLaser);
    }

    private void OnDisable()
    {
        RemoveMethodFromDelegate(PlayerMovement);
        RemoveMethodFromDelegate(PlayerJump);
        RemoveMethodFromDelegate(isPlayerJumpOverTheLaser);
    }
    public void AddMethodToDelegate(PlayerActions method)
    {
        playerActions += method;
    }

    public void RemoveMethodFromDelegate(PlayerActions method)
    {
        playerActions -= method;
    }

    void PlayerMovement()
    {
        float mH = Input.GetAxis(Constants.HORIZONTAL);
        float mV = Input.GetAxis(Constants.VERTICAL);
      
        rb_.velocity = new Vector3(mH * speed, rb_.velocity.y, mV * speed);
        float xPos = transform.position.x;
        float zPos = transform.position.z;

        transform.position = new Vector3(Mathf.Clamp(xPos, 63, 71), transform.position.y, Mathf.Clamp(zPos, -15, -1));
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb_.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }

       
    }
    private void isPlayerJumpOverTheLaser()
    {
        timer_ += Time.deltaTime;
        if (Mathf.Abs(laserTransform.position.z - transform.position.z) < 1f && !isGrounded  && timer_ >= 1f)
        {
            UIManager.instance.AddScore();
            
           
            timer_ = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag(Constants.LASER_TAG))
        {
            UIManager.instance.LoseGame();
        }
    }

    GameObject Coin;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_AREA_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(Constants.COIN_TAG))
        {
            
            UIManager.instance.AddScore();
            Coin = collision.gameObject;
            Coin.SetActive(false);
            StartCoroutine(WaitAndActivateCoin());
        }
    }

    private IEnumerator WaitAndActivateCoin()
    {
        float waitDuration = 3f;
        yield return new WaitForSeconds(waitDuration);
        float randomXPos = UnityEngine.Random.Range(63, 71);
        float randomYPos = UnityEngine.Random.Range(1, 3);
        float randomZPos = UnityEngine.Random.Range(-15, -1);
        Coin.transform.position = new Vector3(randomXPos, randomYPos, randomZPos );
        Coin.SetActive(true);


    }
}
