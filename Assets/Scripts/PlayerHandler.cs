using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private SpriteRenderer skinImg;
    private const int _gyroType = 1;
    private const float gravitySize = 9.81f;

    void Awake(){
        if(gameObject.CompareTag("Player")){
            skinImg = GetComponent<SpriteRenderer>();

            string skinName = PlayerPrefs.GetString("skinName", "character_0");
            skinImg.sprite = Resources.Load<Sprite>($"Images/{skinName}");
        }
    }

    void Start(){
        if(PlayerPrefs.GetInt("mazeMoveType") == (int)MazeMoveHandler.MoveType.Gyro){
            Input.gyro.enabled = true;
        }
        else{
            Physics2D.gravity = Vector2.down * gravitySize;
        }
    }

    void Update(){
        if(Time.deltaTime != 0){
            if(PlayerPrefs.GetInt("mazeMoveType") == (int)MazeMoveHandler.MoveType.Gyro){
                Physics2D.gravity = Input.gyro.gravity.normalized * gravitySize;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.CompareTag("DoorController")){
            GameManager.Instance.isDoorOpening = true;
        }
        if(gameObject.CompareTag("Player") && other.gameObject.CompareTag("Finish")){
            GameManager.Instance.isPlayer1_arrived = true;
        }
        if(gameObject.CompareTag("Player2") && other.gameObject.CompareTag("Finish2")){
            GameManager.Instance.isPlayer2_arrived = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("DoorController")){
            GameManager.Instance.isDoorOpening = false;
        }
        if(gameObject.CompareTag("Player") && other.gameObject.CompareTag("Finish")){
            GameManager.Instance.isPlayer1_arrived = false;
        }
        if(gameObject.CompareTag("Player2") && other.gameObject.CompareTag("Finish2")){
            GameManager.Instance.isPlayer2_arrived = false;
        }
    }

}
