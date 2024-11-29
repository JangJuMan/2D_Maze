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
    public int numOfPlayer;
    

    private const float _timerStar1stPosition_x = -23.7f;
    private const float _timerSliderSize = 380.0f;
    private const int _fullStar0 = 0;
    private const int _fullStar1 = 1;
    private const int _fullStar2 = 2;
    private const int _emptyStar0 = 3;
    private const int _emptyStar1 = 4;
    private const int _emptyStar2 = 5;
    private const int stageMaxHint = 3;

    float time = 0.0f;
    float coroutineInterval = 0.04f;
    int hintLevel = 0;
    int userHintCnt = 0;
    public bool isPlayer1_arrived = false;
    public bool isPlayer2_arrived = false;


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
        switch(numOfPlayer){
            case 1:
                if(isPlayer1_arrived){
                    GameOver();
                }
                break;
            case 2:
                if(isPlayer1_arrived && isPlayer2_arrived){
                    GameOver();
                }
                break;
        }
        isPlayer1_arrived = false;
        isPlayer2_arrived = false;
    }

    void SetTimerStarsPosition(){
        timerStarImages[_fullStar0].rectTransform.anchoredPosition = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[_emptyStar0].rectTransform.anchoredPosition = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[_fullStar1].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[1]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[_emptyStar1].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[1]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[_fullStar2].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[0]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
        timerStarImages[_emptyStar2].rectTransform.anchoredPosition = new Vector2(_timerSliderSize * ((stageGrade[2] - stageGrade[0]) / stageGrade[2]) + _timerStar1stPosition_x, 0);
    }

    // Timer coroutine
    IEnumerator SetTimer(){
        while(true){
            timeText.text = time.ToString("N1");
            timerSlider.value = time / stageGrade[2];

            if(time > stageGrade[2]){
                timerStarImages[_fullStar0].gameObject.SetActive(false);
                timerStarImages[_emptyStar0].gameObject.SetActive(true);
            }
            else if(time > stageGrade[1]){
                timerStarImages[_fullStar1].gameObject.SetActive(false);
                timerStarImages[_emptyStar1].gameObject.SetActive(true);
            }
            else if(time > stageGrade[0]){
                timerStarImages[_fullStar2].gameObject.SetActive(false);
                timerStarImages[_emptyStar2].gameObject.SetActive(true);
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
            starImages[_fullStar0].gameObject.SetActive(true);
            starImages[_fullStar1].gameObject.SetActive(true);
            starImages[_fullStar2].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[1]){
            starImages[_fullStar0].gameObject.SetActive(true);
            starImages[_fullStar1].gameObject.SetActive(true);
            starImages[_emptyStar2].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[2]){
            starImages[_fullStar0].gameObject.SetActive(true);
            starImages[_emptyStar1].gameObject.SetActive(true);
            starImages[_emptyStar2].gameObject.SetActive(true);
        }
        else{
            starImages[_emptyStar0].gameObject.SetActive(true);
            starImages[_emptyStar1].gameObject.SetActive(true);
            starImages[_emptyStar2].gameObject.SetActive(true);
        }
    }

    // Hint
    public string HintCheck(){
        userHintCnt = PlayerPrefs.GetInt("userHintCnt", 0);
        hintCntTxt.text = "(현재 보유 힌트 : " + userHintCnt +  "개)";

        if(hintLevel < stageMaxHint){
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
