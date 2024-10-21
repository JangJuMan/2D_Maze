using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PopupHandler clearPopup;
    public PopupHandler hintPopup;

    public Text timeText;
    public Text hintCntTxt;
    public Image[] starImages;
    public float[] stageGrade;
    public GameObject[] hintWay;

    float time = 0.0f;
    int hintLevel = 0;
    int userHintCnt = 0;
    int defaultHintCnt = 3;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    void Start(){
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }





    // Game Over
    public void GameOver(){
        Time.timeScale = 0.0f;
        SetStars();
        clearPopup.Show();
    }

    private void SetStars(){
        for(int i=0; i<starImages.Length; i++){
            starImages[i].gameObject.SetActive(false);
        }

        if(time <= stageGrade[0]){
            starImages[0].gameObject.SetActive(true);
            starImages[1].gameObject.SetActive(true);
            starImages[2].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[1]){
            starImages[0].gameObject.SetActive(true);
            starImages[1].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[2]){
            starImages[0].gameObject.SetActive(true);
            starImages[4].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
        else{
            starImages[3].gameObject.SetActive(true);
            starImages[4].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
    }

    // Hint
    public void SetUserHintCnt(){
        userHintCnt = PlayerPrefs.GetInt("userHintCnt", defaultHintCnt);
        hintCntTxt.text = "(현재 보유 힌트 : " + userHintCnt +  "개)";
    }

    public void UseHint(){
        if(userHintCnt > 0){
            if(hintLevel < 3){
                hintWay[hintLevel++].SetActive(true);
                PlayerPrefs.SetInt("userHintCnt", --userHintCnt);
                Debug.Log("힌트 사용 완료");
            }
            else{
                Debug.Log("해당 스테이지에서 힌트를 모두 사용하셨습니다");
            }
        }
        else{
            Debug.Log("사용 가능한 힌트가 없습니다");
        }
    }
}
