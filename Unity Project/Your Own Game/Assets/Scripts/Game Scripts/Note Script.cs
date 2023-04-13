using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject note;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        note.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    public void ShowNote()
    {
        note.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void HideNote()
    {
        note.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
