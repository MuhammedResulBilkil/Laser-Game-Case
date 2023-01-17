using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public
    public static PlayerController instance;
    public float speed;
    public float jumpForce;
    public bool isJumping;
   
   
    #endregion
    #region Serialize
    [SerializeField] private Transform laserTransform;
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private Animator anim;


    #endregion
    #region References

    private float timer_;
    private Rigidbody rb_;
  
    private bool isCollectCoin = false;
    private bool isGrounded = false;
    private GameObject Coin;
    private Vector3 targetCoinPosition;
    private float durationForCollectCoin;
    private Touch touch;
 
    #endregion

    #region Delegate Method
    public delegate void PlayerActions();
    public PlayerActions playerActions;
    #endregion


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //reach rigidbody component.
        rb_ = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        targetCoinPosition = new Vector3(60.26f, 13.3f, -11);
        durationForCollectCoin = 5;
      
        //this timer will use as a boolean variable.
        timer_ = 0;
    }


    private void Update()
    {

        PlayerActionsMethod();
        CoinAnimation();


    }

    private void CoinAnimation()
    {
        if (isCollectCoin)
        {

            //if isCollectionCoin is true , so change this coin position(animation to move coin to score text )
            Coin.transform.position = Vector3.Lerp(Coin.transform.position, targetCoinPosition, durationForCollectCoin * Time.deltaTime);
        }
    }
    public void PlayerActionsMethod()
    {
        //delegate method null control 
        if(playerActions!=null)
        {
            playerActions();
        }
       
    }

    private void OnEnable()
    {
        //add this methods to Player Action.
        AddMethodToPlayerAction(PlayerMovement);
        AddMethodToPlayerAction(PlayerJump);
        AddMethodToPlayerAction(isPlayerJumpOverTheLaser);
    }

    private void OnDisable()
    {
        //remove this methods from Player Action.
        RemoveMethodFromPlayerAction(PlayerMovement);
        RemoveMethodFromPlayerAction(PlayerJump);
        RemoveMethodFromPlayerAction(isPlayerJumpOverTheLaser);
    }

    //function for add method to delegate.
    public void AddMethodToPlayerAction(PlayerActions method)
    {
        playerActions += method;
    }

    //function for remove method from delegate.
    public void RemoveMethodFromPlayerAction(PlayerActions method)
    {
        playerActions -= method;
    }
 
    void PlayerMovement()
    {

        rb_.velocity = new Vector3(joyStick.Horizontal * speed, rb_.velocity.y, joyStick.Vertical * speed);
        if(joyStick.Horizontal !=0 || joyStick.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(rb_.velocity);
            anim.SetBool(Constants.RUNNING_ANIM, true);
        }
        else
        {
            anim.SetBool(Constants.RUNNING_ANIM, false);
        }

         float xPos = transform.position.x;
         float zPos = transform.position.z;
        //limits of area that player can walk.
          transform.position = new Vector3(Mathf.Clamp(xPos, 63, 71), transform.position.y, Mathf.Clamp(zPos, -15, -1));

    }

    void PlayerJump()
    {
        if (isJumping && isGrounded)
        {
            rb_.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            //sound effect for jump
            PlayAnimationAndSoundForJump();
            //make sure player can not jump in air.
            isGrounded = false;
            isJumping = false;
            
        }

        else
        {
            anim.SetBool(Constants.JUMPING_ANIM, false);
        }

       
    }
    private void isPlayerJumpOverTheLaser()
    {
        timer_ += Time.deltaTime;
        //if laser and player z positions are same and player is in air, add score.
        //I use distance because laser is moving fast and player and laser can not being in same z posiiton at same time.
        float distance = Mathf.Abs(laserTransform.position.z - transform.position.z);
        if (distance < 1f && !isGrounded  && timer_ >= 1f)
        {
            UIManager.instance.AddScore();
            
           
            timer_ = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if player touch the laser or ball, player loses game.
       if(other.gameObject.CompareTag(Constants.LASER_TAG) )
        {
            anim.SetBool(Constants.DEAD_ANIM, true);
            PlayAnimationAndSoundForDie();
            //wait and game over(we are waiting for die animaton finished)
            StartCoroutine(WaitForDeadAnimationFinished(1.7f));
           
           
        }

       //if player touch the coin , add score then move the coin to score text area(animation codes), then deactivate coin because we ' r gonna use this coin again.
       // I didnt want to use Instantiate-Destroy circle. So if i touch the coin , coin will be deactivate then it will activate another position .
       //But this position have to be in Player area.
        if (other.gameObject.CompareTag(Constants.COIN_TAG))
        {
            PlaySoundForCollectCoins();
            isCollectCoin = true;
            Coin = other.gameObject;
            StartCoroutine(WaitAndDeactivateCoin(1));
           
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //if player touch the player area(ground) , player can jump.
        if(collision.gameObject.CompareTag(Constants.PLAYER_AREA_TAG))
        {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag(Constants.BALL_TAG))
        {
           
            PlayAnimationAndSoundForDie();          
            StartCoroutine(WaitForDeadAnimationFinished(1.7f));


        }
       
    }


   

    private void PlaySoundForCollectCoins()
    {
        SoundEffectManager.instance.PlayAudioClip(SoundEffectManager.instance.clips[0]);
       
    }
    private void PlayAnimationAndSoundForJump()
    {
        //animation for jump
        anim.SetBool(Constants.JUMPING_ANIM, true);
        SoundEffectManager.instance.PlayAudioClip(SoundEffectManager.instance.clips[1]);
    }
    private void PlayAnimationAndSoundForDie()
    {
        anim.SetBool(Constants.DEAD_ANIM, true);
        SoundEffectManager.instance.PlayAudioClip(SoundEffectManager.instance.clips[2]);
        //if we die and laser touch us again , we have to prevent the sound effect for die.
        SoundEffectManager.instance.isSoundOff = true;
    }


    private IEnumerator WaitAndDeactivateCoin(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        UIManager.instance.AddScore();
        Coin.SetActive(false);
        isCollectCoin = false;
        StartCoroutine(WaitAndActivateCoin(3));


    }
    private IEnumerator WaitAndActivateCoin(float waitDuration)
    {

        yield return new WaitForSeconds(waitDuration);
        float randomXPos = UnityEngine.Random.Range(63, 71);
        float randomYPos = UnityEngine.Random.Range(1, 3);
        float randomZPos = UnityEngine.Random.Range(-15, -1);
        Coin.transform.position = new Vector3(randomXPos, randomYPos, randomZPos);
        Coin.SetActive(true);


    }
    private IEnumerator WaitForDeadAnimationFinished(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        
       
        //after die animation finished, game will be over.
        UIManager.instance.LoseGame();

    }
}
