using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.001f, 0.01f)]
    public float bobAmount = 0.002f;
    [Range(1f, 30f)]
    public float bobFrequency = 10f;
    [Range(10f, 100f)] 
    public float bobSmooth = 10.0f;

    [Range(0.001f, 0.01f)]
    public float bobRunAmount = 0.002f;
    [Range(1f, 30f)]
    public float bobRunFrequency = 10f;
    [Range(10f, 100f)] 
    public float bobRunSmooth = 10.0f;
    public PlayerMovement playermovement;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadBobTrigger();
    }

    private void CheckForHeadBobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).magnitude;
        
        if(inputMagnitude > 0 && playermovement.GetisGrounded())
        {
            if(playermovement.GetisRunning())
            {
                StartHeadBobRun();
            }
            else
            {
                StartHeadBob();
            }

        }
        StopHeadBob();
    }
    private Vector3 StartHeadBob()
    {
        Vector3 position = Vector3.zero;
        position.y += Mathf.Lerp(position.y, Mathf.Sin(Time.time * bobFrequency) * bobAmount * 1.4f, bobSmooth * Time.deltaTime);
        position.x += Mathf.Lerp(position.x, Mathf.Sin(Time.time * bobFrequency / 2f) * bobAmount * 1.6f, bobSmooth * Time.deltaTime);
        transform.localPosition += position;

        return position;
    }

    private void StopHeadBob()
    {
        if(transform.localPosition == startPosition)
        {
            return;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, 1 * Time.deltaTime);

    }

    private Vector3 StartHeadBobRun()
    {
        Vector3 position = Vector3.zero;
        position.y += Mathf.Lerp(position.y, Mathf.Sin(Time.time * bobRunFrequency) * bobRunAmount * 1.4f, bobRunSmooth * Time.deltaTime);
        position.x += Mathf.Lerp(position.x, Mathf.Sin(Time.time * bobRunFrequency / 2f) * bobRunAmount * 1.6f, bobRunSmooth * Time.deltaTime);
        transform.localPosition += position;

        return position;
    }

    private void StopHeadBobRun()
    {
        if(transform.localPosition == startPosition)
        {
            return;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, 1 * Time.deltaTime);

    }
}
