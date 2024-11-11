using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private float gravitySize = 9.81f;

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
