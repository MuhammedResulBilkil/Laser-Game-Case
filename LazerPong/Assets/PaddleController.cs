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
    
    private bool isTouchedPaddle=false;
    #endregion


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
            StartCoroutine(WaitAndStopThePaddleMovement());
        }
    }

    
   
    IEnumerator WaitAndStopThePaddleMovement()
    {
        yield return new WaitForSeconds(waitDuration);
        isTouchedPaddle = false;
    }

   
}
