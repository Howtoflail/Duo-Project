using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float destroyDelay;
    [SerializeField]
    private string collectableName;

    [SerializeField]
    private AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetCollectableName()
    {
        audioSource.Play();
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, destroyDelay);
        return collectableName;
    }
}
