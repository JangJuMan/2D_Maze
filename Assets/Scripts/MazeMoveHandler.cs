using UnityEngine;

public class MazeMove : MonoBehaviour
{
    private float move_speed = 0.01f;
    private float touch_speed = 0.1f;
    private float mouse_speed = 1.0f;
    // private float keyboard_speed = 1.0f;
    private float btn_speed = 1.0f;
    private bool isLeftBtnDown = false;
    private bool isRightBtnDown = false;

    public enum MoveType{Drag, Gyro, Btn}

    void Update(){
        if(Time.deltaTime != 0){
            ViewControl();
            MazeControl();
        }
    }

    public void ViewControl(){
        if(Input.touchCount == 2){
            // Camera Zoom (CameraHandler.cs)
            // Maze Move(MazeMoveHandler.cs)
            float moveX = (Input.GetTouch(0).deltaPosition.x + Input.GetTouch(1).deltaPosition.x) * move_speed * 0.5f;
            float moveY = (Input.GetTouch(0).deltaPosition.y + Input.GetTouch(1).deltaPosition.y) * move_speed * 0.5f;
            transform.Translate(moveX, moveY, 0, Space.World);
        }
    }

    public void MazeControl(){
        int mazeMoveType = PlayerPrefs.GetInt("mazeMoveType");
        switch(mazeMoveType){
            case (int)MoveType.Drag:
                #if UNITY_EDITOR
                    // FOR DEBUG : PC 디버그용
                    if(Input.GetMouseButton(0)){
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse X") * mouse_speed, Space.World);
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouse_speed, Space.World);
                    }
                    break;
                #else
                    if(Input.touchCount == 1){ // For Mobile
                        transform.Rotate(0f, 0f, Input.GetTouch(0).deltaPosition.x * touch_speed, Space.World);
                        transform.Rotate(0f, 0f, Input.GetTouch(0).deltaPosition.y * touch_speed, Space.World);
                    }
                #endif
            case (int)MoveType.Btn:
                if(isLeftBtnDown){
                    transform.Rotate(0f, 0f, btn_speed, Space.World);
                }
                if(isRightBtnDown){
                    transform.Rotate(0f, 0f, -btn_speed, Space.World);
                }
                break;
            }
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
