using UnityEngine;

public class MazeMove : MonoBehaviour
{
    private float touch_speed = 0.1f;
    private float mouse_speed = 1.0f;
    // private float keyboard_speed = 1.0f;
    private float btn_speed = 1.0f;
    private bool isLeftBtnDown = false;
    private bool isRightBtnDown = false;

    void Awake(){
        
    }

    void OnEnable(){
    }

    void Start()
    {
    }

    void Update(){
        if(Time.deltaTime != 0){
            int mazeMoveType = PlayerPrefs.GetInt("mazeMoveType");
            switch(mazeMoveType){
                case 0:
                    if(Input.touchCount == 1){ // For Mobile
                        transform.Rotate(0f, 0f, Input.GetTouch(0).deltaPosition.x * touch_speed, Space.World);
                        transform.Rotate(0f, 0f, Input.GetTouch(0).deltaPosition.y * touch_speed, Space.World);
                    }
                    else if(Input.touchCount == 2){
                        // For Camera zoom Control (CanaraHandler.cs)
                    }
                    else if(Input.GetMouseButton(0)){ // For PC
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse X") * mouse_speed, Space.World);
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouse_speed, Space.World);
                    }
                    break;
                case 2:
                    if(isLeftBtnDown){
                        transform.Rotate(0f, 0f, btn_speed, Space.World);
                    }
                    if(isRightBtnDown){
                        transform.Rotate(0f, 0f, -btn_speed, Space.World);
                    }
                    // if(Input.GetButton("Horizontal")){
                    //     transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * keyboard_speed, Space.World);
                    // }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    void LateUpdate(){
    }

    void OnDisable(){
    }

    void OnDestroy(){
    }
    
    public void LeftBtnDown(){
        isLeftBtnDown = true;
    }
    public void LeftBtnUp(){
        isLeftBtnDown = false;
    }
    public void RightBtnDown(){
        isRightBtnDown = true;
    }
    public void RightBtnUp(){
        isRightBtnDown = false;
    }
}
