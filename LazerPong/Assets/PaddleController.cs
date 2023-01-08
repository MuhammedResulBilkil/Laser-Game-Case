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
        //Debug.Log(Mathf.Abs(distance));
        if ( Mathf.Abs(distance) > 20 && !isTouchedPaddle)
        {
            
            zPos = PlayerTransform.position.z;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, zPos), 1);
           // transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
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
