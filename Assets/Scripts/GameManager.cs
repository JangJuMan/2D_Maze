using System.Collections;
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
    public Image[] timerStarImages;
    public float[] stageGrade;
    public GameObject[] hintWay;
    public Slider timerSlider;
    

    private const float _timerStar1stPosition_x = -23.7f;
    private const float _timerSliderSize = 380.0f;
    float time = 0.0f;
    float coroutineInterval = 0.04f;
    int hintLevel = 0;
    int userHintCnt = 0;


    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
        SetTimerStarsPosition();
    }

    void Start(){
        Time.timeScale = 1.0f;
        StartCoroutine("SetTimer");
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void SetTimerStarsPosition(){
        timerStarImages[0].rectTransform.anchoredPosition = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[3].rectTransform.anchoredPosition = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[1].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[1]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[4].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[1]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[2].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[0]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[5].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[0]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
    }

    // Timer coroutine
    IEnumerator SetTimer(){
        while(true){
            timeText.text = time.ToString("N1");
            timerSlider.value = time / stageGrade[2];

            if(time > stageGrade[2]){
                timerStarImages[0].gameObject.SetActive(false);
                timerStarImages[3].gameObject.SetActive(true);
            }
            else if(time > stageGrade[1]){
                timerStarImages[1].gameObject.SetActive(false);
                timerStarImages[4].gameObject.SetActive(true);
            }
            else if(time > stageGrade[0]){
                timerStarImages[2].gameObject.SetActive(false);
                timerStarImages[5].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(coroutineInterval);
        }
    }

    // Game Over
    public void GameOver(){
        StopCoroutine("SetTimer");
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
    public string HintCheck(){
        userHintCnt = PlayerPrefs.GetInt("userHintCnt", 0);
        hintCntTxt.text = "(현재 보유 힌트 : " + userHintCnt +  "개)";

        if(hintLevel < 3){
            if(userHintCnt > 0){
                return "CanUseHint";
            }
            return "NoHint";
        }
        return "AlreadyUsed";
    }

    public void UseHint(){
        hintWay[hintLevel++].SetActive(true);
        PlayerPrefs.SetInt("userHintCnt", --userHintCnt);
        Debug.Log("힌트 사용 완료");
    }
}
