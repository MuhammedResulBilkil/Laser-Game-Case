using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float zPos;
    [SerializeField] Transform PlayerTransform;

    private void Update()
    {
        zPos = PlayerTransform.position.z;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
