using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = 1.0000001f;

        if (Input.GetKey(KeyCode.I))
        {
            transform.position += Vector3.up * multiplier;
        }
        if(Input.GetKey(KeyCode.K)) 
        {
            transform.position += Vector3.down * multiplier;
        }
        if(Input.GetKey(KeyCode.J)) 
        {
            transform.position += Vector3.left * multiplier;
        }
        if(Input.GetKey(KeyCode.L)) 
        {
            transform.position += Vector3.right * multiplier;
        }
    }
}
