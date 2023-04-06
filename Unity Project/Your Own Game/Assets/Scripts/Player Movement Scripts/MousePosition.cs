using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 4f;
    }
}
