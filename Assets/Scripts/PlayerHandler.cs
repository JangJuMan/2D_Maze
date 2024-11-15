using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private SpriteRenderer skinImg;
    private float gravitySize = 9.81f;

    void Awake(){
        skinImg = GetComponent<SpriteRenderer>();

        string skinName = PlayerPrefs.GetString("skinName", "character_0");
        skinImg.sprite = Resources.Load<Sprite>($"Images/{skinName}");
    }

    void Start(){
        Input.gyro.enabled = true;
    }

    void Update(){
        if(Time.deltaTime != 0){
            if(PlayerPrefs.GetInt("mazeMoveType") == 1){
                Physics2D.gravity = Input.gyro.gravity.normalized * gravitySize;
                Debug.Log(PlayerPrefs.GetInt("mazeMoveType"));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Finish")){
            GameManager.Instance.GameOver();
        }
    }

}
