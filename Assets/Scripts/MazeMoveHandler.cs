 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMove : MonoBehaviour
{
    private float mouse_speed = 2.0f;
    private float keyboard_speed = 0.4f;

    void Awake(){
        
    }

    void OnEnable(){
    }

    void Start()
    {
        // Application.targetFrameRate = 60;
    }

    void FixedUpdate(){
        if(Input.GetMouseButton(0)){
            transform.Rotate(0f, 0f, -Input.GetAxis("Mouse X") * mouse_speed, Space.World);
            transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouse_speed, Space.World);
        }

        if(Input.GetButton("Horizontal")){
            transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * keyboard_speed, Space.World);
        }
    }

    void Update()
    {
        
    }

    void LateUpdate(){
    }

    void OnDisable(){
    }

    void OnDestroy(){
    }
}
