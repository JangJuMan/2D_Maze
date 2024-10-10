using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMove : MonoBehaviour
{
    private float mouse_speed = 1.0f;
    private float keyboard_speed = 1.0f;

    void Awake(){
        Debug.Log("Awake");
        
    }

    void OnEnable(){
        Debug.Log("OnEnable");
    }

    void Start()
    {
        Debug.Log("start");
    }

    void FixedUpdate(){
        Debug.Log("Fixed Update");
        if(Input.GetMouseButton(0)){
            transform.Rotate(0f, 0f, -Input.GetAxis("Mouse X") * mouse_speed, Space.World);
            transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouse_speed, Space.World);
        }

        if(Input.GetButton("Horizontal")){
            Debug.Log("Horizontal Move");
            transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * keyboard_speed, Space.World);
        }
    }

    void Update()
    {
        Debug.Log("update");
        
    }

    void LateUpdate(){
        Debug.Log("LateUpdate");
    }

    void OnDisable(){
        Debug.Log("OnDisable");
    }

    void OnDestroy(){
        Debug.Log("OnDestroy");
    }
}
