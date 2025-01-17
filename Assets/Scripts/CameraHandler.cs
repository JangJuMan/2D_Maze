using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private const float zoomSpeed = 0.01f;
    private const float maxCameraSize = 5.0f;
    private const float minCameraSize = 20.0f;
    private Camera cam;

    void Start(){
        cam = GetComponent<Camera>();
    }

    void FixedUpdate(){
        if(Input.touchCount == 2){
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1_prevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2_prevPos = touch2.position - touch2.deltaPosition;

            float prevTouchDeltaMag = (touch1_prevPos - touch2_prevPos).magnitude;
            float currTouchDeltaMag = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - currTouchDeltaMag;

            cam.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
            cam.orthographicSize = Mathf.Max(cam.orthographicSize, maxCameraSize);
            cam.orthographicSize = Mathf.Min(cam.orthographicSize, minCameraSize);
        }
    }
}
