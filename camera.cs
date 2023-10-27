using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    public Transform target;  
    public Vector3 offset = new Vector3(0.0f, 15.5f, 22.0f); 

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3( target.position.x,18.0f,target.position.z+22.0f) ;
        }
    }
}
