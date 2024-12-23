using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용하지 않는 class
public class LoadingCircle : MonoBehaviour
{
    private RectTransform rectComponent;
    private float rotateSpeed = -200f;

    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.unscaledDeltaTime);
    }
}
