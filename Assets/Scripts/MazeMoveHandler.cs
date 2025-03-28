using UnityEngine;

public class MazeMoveHandler : MonoBehaviour
{
    private const float move_speed = 0.005f;
    private const float touch_speed = 0.1f;
    private const float mouse_speed = 1.0f;
    private const float btn_speed = 1.25f;
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
            // Camera Zoom (CameraHandler.cs에 구현)
            
            // Maze Move
            float moveX = (Input.GetTouch(0).deltaPosition.x + Input.GetTouch(1).deltaPosition.x) * move_speed;
            float moveY = (Input.GetTouch(0).deltaPosition.y + Input.GetTouch(1).deltaPosition.y) * move_speed;
            transform.Translate(moveX, moveY, 0, Space.World);
        }
    }

    public void MazeControl(){
        int mazeMoveType = PlayerPrefs.GetInt("mazeMoveType");
        switch(mazeMoveType){
            case (int)MoveType.Drag:
                #if UNITY_EDITOR        // FOR DEBUG : PC판 디버그용
                    if(Input.GetMouseButton(0)){
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse X") * mouse_speed, Space.World);
                        transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouse_speed, Space.World);
                    }
                    break;
                #else
                    if(Input.touchCount == 1){ // For Mobile
                        Touch touch = Input.GetTouch(0);
                        float rotateX = touch.deltaPosition.x * touch_speed;
                        float rotateY = touch.deltaPosition.y * touch_speed;
                        if(touch.position.x < (Screen.width/2)){
                            rotateY *= -1;
                        }
                        if(touch.position.y > (Screen.height/2)){
                            rotateX *= -1;
                        }
                        transform.Rotate(0f, 0f, rotateX + rotateY, Space.World);
                    }
                    break;
                #endif
            case (int)MoveType.Gyro:
                // PlayerHandler.cs에서 처리함
                break;
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
