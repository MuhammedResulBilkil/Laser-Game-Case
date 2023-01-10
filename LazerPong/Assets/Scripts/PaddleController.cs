using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    #region Serialize
    [SerializeField] Transform BallTransform;
    
    #endregion

    #region References
    private float distance_;
    private Rigidbody rbOfBall_;
    private int currentForceSpeed;
   
    private bool isTouchedPaddle=false;
    #endregion


    private void Awake()
    {
        //reach ball's rigidbody component.
        rbOfBall_ = BallController.instance.GetComponent<Rigidbody>();
        //reach ball's force speed to change in run time.
        currentForceSpeed = BallController.instance.currentForceSpeed;
    }

    void Update()
    {
        //find distance between ball and paddle. Because we have 2 paddle but 1 script . So i have to figre out which paddle will hit the ball.
       distance_ = BallTransform.position.x - transform.position.x;
     
        //if distance >20 and didnt touch the paddle yet, paddle can move.
       if ( Mathf.Abs(distance_) <15 && !isTouchedPaddle)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, BallTransform.position.z);
            float duration =1f;
            transform.position = Vector3.Lerp(transform.position,targetPos,duration);
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if we touch the ball find which paddle is forcing then add force.
        if(collision.gameObject.CompareTag(Constants.BALL_TAG))
        {
            isTouchedPaddle = true;
            WhichPaddleIsForcing();
            //wait 2 seconds and make sure paddle can not move anymore. If we dont do that , after hitting ball, the paddle continues to follow the position of ball.
            StartCoroutine(WaitAndStopThePaddleMovement(2.5f));
        }
    }
    
    private void WhichPaddleIsForcing()
    {
       
        float xPos = transform.position.x;
        if(xPos > 60)
        {
            //red paddle
            Vector3 direction = new Vector3(-1, 0, 0);
            AddForce(direction);
        }
        else
        {
            //blue paddle
            Vector3 direction = new Vector3(1, 0, 0);
            AddForce(direction);
        }
    }

    private void AddForce(Vector3 v)
    {
        //add force to ball.
        Vector3 velocity = rbOfBall_.velocity;
        velocity.x = 0f;
        rbOfBall_.velocity = velocity;
        rbOfBall_.AddForce(v * currentForceSpeed, ForceMode.Impulse);
    }
    
   
    IEnumerator WaitAndStopThePaddleMovement(float waitDuration)
    {
        
        yield return new WaitForSeconds(waitDuration);
        isTouchedPaddle = false;
    }

   
}
