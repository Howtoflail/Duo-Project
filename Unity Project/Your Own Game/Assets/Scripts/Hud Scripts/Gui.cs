using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("Level 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
