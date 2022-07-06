using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public float paralaxEffect;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * paralaxEffect;
        lastCameraPosition = cameraTransform.position;
    }
}
