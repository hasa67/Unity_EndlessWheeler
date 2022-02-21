using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraDistance;
    public Transform targetObject;

    private Vector3 targetPosition;
    private Vector3 cameraPosition;


    void Update() {
        targetPosition = targetObject.position;
        cameraPosition = transform.position;

        cameraPosition.z = targetPosition.z - cameraDistance;

        transform.position = cameraPosition;
    }
}
