using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    #region Serialize
    [SerializeField] Transform BallTransform;
    [SerializeField] float waitDuration = 2f;
    #endregion

    #region References
    private float distance_;
    private Rigidbody rbOfBall_;
    private int currentForceSpeed;
    
    private bool isTouchedPaddle=false;
    #endregion


    private void Awake()
    {
        rbOfBall_ = BallController.instance.GetComponent<Rigidbody>();
        currentForceSpeed = BallController.instance.currentForceSpeed;
    }

    void Update()
    {
       distance_ = BallTransform.position.x + transform.position.x;
     
       if ( Mathf.Abs(distance_) > 20 && !isTouchedPaddle)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,transform.position.y, BallTransform.position.z), 1);
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Constants.BALL_TAG))
        {
            isTouchedPaddle = true;
            WhichPaddleIsForcing();
            //
            // 


            StartCoroutine(WaitAndStopThePaddleMovement());
        }
    }
    
    private void WhichPaddleIsForcing()
    {
       
        float xPos = transform.position.x;
        if(xPos > 60)
        {
            Vector3 direction = new Vector3(-1, 0, 0);
            AddForce(direction);
        }
        else
        {
            Vector3 direction = new Vector3(1, 0, 0);
            AddForce(direction);
        }
    }

    private void AddForce(Vector3 v)
    {
        Vector3 velocity = rbOfBall_.velocity;
        velocity.x = 0f;
        rbOfBall_.velocity = velocity;
        rbOfBall_.AddForce(v * currentForceSpeed, ForceMode.Impulse);
    }
    
   
    IEnumerator WaitAndStopThePaddleMovement()
    {
        yield return new WaitForSeconds(waitDuration);
        isTouchedPaddle = false;
    }

   
}
