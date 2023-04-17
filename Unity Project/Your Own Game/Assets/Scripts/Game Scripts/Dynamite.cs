using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dynamite : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float loadOutlineDistance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Outline outline;
    private bool isPlaced;

    void Start()
    {
        isPlaced = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        outline = gameObject.GetComponent<Outline>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (isPlaced)
        {
            outline.enabled = false;
            SceneManager.LoadScene("Game End");
        }
        else
        {
            if (
                Vector3.Distance(transform.position, player.transform.position)
                >= loadOutlineDistance
            )
            {
                outline.enabled = false;
            }
            else
            {
                outline.enabled = true;
            }
        }
    }

    public void EnableMesh()
    {
        isPlaced = true;
    }
}
