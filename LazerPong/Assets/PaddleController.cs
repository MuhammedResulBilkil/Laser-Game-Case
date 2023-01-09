using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    public float waitDuration = 2f;
    float distance;
    float zPos;
    bool isTouchedPaddle=false;
 
    void Update()
    {
        distance = PlayerTransform.position.x + transform.position.x;
     
       if ( Mathf.Abs(distance) > 20 && !isTouchedPaddle)
         {

            // transform.position = new Vector3(transform.position.x, transform.position.y,  Mathf.Lerp(zPos, PlayerTransform.position.z, 1));         
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,transform.position.y,PlayerTransform.position.z), 1);
        }

      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ball")
        {
            isTouchedPaddle = true;
            StartCoroutine(c());
        }
    }

    
    //exit => true
    IEnumerator c()
    {
        yield return new WaitForSeconds(waitDuration);
        isTouchedPaddle = false;
    }

   
}
