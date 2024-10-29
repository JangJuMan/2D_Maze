using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float zoomSpeed = 0.5f;
    private Camera cam;

    void Start(){
        cam = GetComponent<Camera>();
    }

    void Update(){
        if(Input.touchCount == 2){
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1_prevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2_prevPos = touch2.position - touch2.deltaPosition;

            float prevTouchDeltaMag = (touch1_prevPos - touch2_prevPos).magnitude;
            float currTouchDeltaMag = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - currTouchDeltaMag;

            cam.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
            cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
        }
    }













    // private float zoomSpeed = 3.0f;
    // private float minZoomSize = 3.0f;
    // private float maxZoomSize = 10.0f;

    // private float targetZoomSize;
    // private Camera cam;

    // void Start(){
    //     cam = GetComponent<Camera>();
    //     targetZoomSize = cam.orthographicSize;
    // }

    // void Update(){
    //     ControllerZoom();

    //     UpdateZoom();
    // }

    // private void ControllerZoom(){
    //     var scrollInput = Input.GetAxis("Mouse ScrollWheel");
    //     var hasScrollInput = Mathf.Abs(scrollInput) > Mathf.Epsilon;
    //     if(!hasScrollInput){
    //         return;
    //     }

    //     var newSize = cam.orthographicSize - scrollInput * zoomSpeed;

    //     targetZoomSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
    // }

    // private void UpdateZoom(){
    //     if(Math.Abs(targetZoomSize - cam.orthographicSize) < Mathf.Epsilon){
    //         return;
    //     }

    //     var mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
    //     var cameraTransform = transform;
    //     var currCameraPos = cameraTransform.position;
    //     var offsetCamera = mouseWorldPos - currCameraPos - (mouseWorldPos - currCameraPos) / (cam.orthographicSize / targetZoomSize);

    //     cam.orthographicSize = targetZoomSize;

    //     currCameraPos += offsetCamera;
    //     cameraTransform.position = currCameraPos;
    // }
}
