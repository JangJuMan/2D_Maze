using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject door;
    public int numOfPlayer;
    public bool isPlayer1_arrived = false;
    public bool isPlayer2_arrived = false;
    public bool isDoorOpening = false;
    public int currStage;
    

    private const float _timerStar1stPosition_x = -23.7f;
    private const float _timerSliderSize = 380.0f;
    private const float _stop = 0.0f;
    private const float _resume = 1.0f;
    private const float _coroutineInterval = 0.1f;
    private const int _defaultStage = 1;
    private const int _stageMaxHint = 3;

    private float time = 0.0f;
    private int hintLevel = 0;
    private int userHintCnt = 0;
    private double doorMoveDist = 0;

    private enum Stars {Full0, Full1, Full2, Empty0, Empty1, Empty2}
    private enum Grade {RankA, RankB, RankC, RankD}
    public enum HintType {CanUseHint, NoHint, AlreadyUsed}


    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
        SetTimerStarsPosition();
    }

    void Start(){
        Time.timeScale = _resume;
        StartCoroutine("SetTimer");
    }

    void Update()
    {
        time += Time.deltaTime;

        if(Time.timeScale != _stop){
            if(isDoorOpening){
                OpenDoor();
            }
            else{
                CloseDoor();
            }
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
        }
    }

    void OpenDoor(){
        if(doorMoveDist < door.transform.localScale.x){
            door.transform.Translate(Vector2.left * Time.deltaTime);
            doorMoveDist += Time.deltaTime;
        }
    }

    void CloseDoor(){
        if(doorMoveDist > 0.0f){
            door.transform.Translate(Vector2.right * Time.deltaTime);
            doorMoveDist -= Time.deltaTime;
        }
    }

    void SetTimerStarsPosition(){
        timerStarImages[(int)Stars.Full0].rectTransform.anchoredPosition
             = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[(int)Stars.Empty0].rectTransform.anchoredPosition
             = new Vector2(_timerStar1stPosition_x, 0);
        timerStarImages[(int)Stars.Full1].rectTransform.anchoredPosition
             = new Vector2(_timerSliderSize * ((stageGrade[(int)Grade.RankC] - stageGrade[(int)Grade.RankB]) / stageGrade[(int)Grade.RankC]) + _timerStar1stPosition_x, 0);
        timerStarImages[(int)Stars.Empty1].rectTransform.anchoredPosition
             = new Vector2(_timerSliderSize * ((stageGrade[(int)Grade.RankC] - stageGrade[(int)Grade.RankB]) / stageGrade[(int)Grade.RankC]) + _timerStar1stPosition_x, 0);
        timerStarImages[(int)Stars.Full2].rectTransform.anchoredPosition
             = new Vector2(_timerSliderSize * ((stageGrade[(int)Grade.RankC] - stageGrade[(int)Grade.RankA]) / stageGrade[(int)Grade.RankC]) + _timerStar1stPosition_x, 0);
        timerStarImages[(int)Stars.Empty2].rectTransform.anchoredPosition
             = new Vector2(_timerSliderSize * ((stageGrade[(int)Grade.RankC] - stageGrade[(int)Grade.RankA]) / stageGrade[(int)Grade.RankC]) + _timerStar1stPosition_x, 0);
    }

    // Timer coroutine
    IEnumerator SetTimer(){
        while(true){
            timeText.text = time.ToString("N1");
            timerSlider.value = time / stageGrade[2];

            if(time > stageGrade[(int)Grade.RankC]){
                timerStarImages[(int)Stars.Full0].gameObject.SetActive(false);
                timerStarImages[(int)Stars.Empty0].gameObject.SetActive(true);
            }
            else if(time > stageGrade[(int)Grade.RankB]){
                timerStarImages[(int)Stars.Full1].gameObject.SetActive(false);
                timerStarImages[(int)Stars.Empty1].gameObject.SetActive(true);
            }
            else if(time > stageGrade[(int)Grade.RankA]){
                timerStarImages[(int)Stars.Full2].gameObject.SetActive(false);
                timerStarImages[(int)Stars.Empty2].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(_coroutineInterval);
        }
    }

    // Game Over
    public void GameOver(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Clear);
        Time.timeScale = _stop;
        StopCoroutine("SetTimer");
        
        string sceneName = SceneManager.GetActiveScene().name;

        SetStars(sceneName);
        UpdateCurrStage();
        clearPopup.Show();
    }

    private void UpdateCurrStage(){
        if(++currStage > PlayerPrefs.GetInt("currStage", _defaultStage)){
            PlayerPrefs.SetInt("currStage", currStage);
            PlayerPrefs.Save();
        }
    }

    private void SetStars(string currSceneName){
        int grade = -1;
        for(int i=0; i<starImages.Length; i++){
            starImages[i].gameObject.SetActive(false);
        }

        if(time <= stageGrade[0]){
            starImages[(int)Stars.Full0].gameObject.SetActive(true);
            starImages[(int)Stars.Full1].gameObject.SetActive(true);
            starImages[(int)Stars.Full2].gameObject.SetActive(true);
            grade = (int)Grade.RankD;
        }
        else if(time <= stageGrade[1]){
            starImages[(int)Stars.Full0].gameObject.SetActive(true);
            starImages[(int)Stars.Full1].gameObject.SetActive(true);
            starImages[(int)Stars.Empty2].gameObject.SetActive(true);
            grade = (int)Grade.RankC;
        }
        else if(time <= stageGrade[2]){
            starImages[(int)Stars.Full0].gameObject.SetActive(true);
            starImages[(int)Stars.Empty1].gameObject.SetActive(true);
            starImages[(int)Stars.Empty2].gameObject.SetActive(true);
            grade = (int)Grade.RankB;
        }
        else{
            starImages[(int)Stars.Empty0].gameObject.SetActive(true);
            starImages[(int)Stars.Empty1].gameObject.SetActive(true);
            starImages[(int)Stars.Empty2].gameObject.SetActive(true);
            grade = (int)Grade.RankA;
        }

        if(grade > PlayerPrefs.GetInt($"stageStars_{currSceneName}", -1)){
            PlayerPrefs.SetInt($"stageStars_{currSceneName}", grade);
        }
    }

    // Hint
    public HintType HintCheck(){
        userHintCnt = PlayerPrefs.GetInt("userHintCnt", 0);
        hintCntTxt.text = "(현재 보유 힌트 : " + userHintCnt +  "개)";

        if(hintLevel < _stageMaxHint){
            if(userHintCnt > 0){
                return HintType.CanUseHint;
            }
            return HintType.NoHint;
        }
        return HintType.AlreadyUsed;
    }

    public void UseHint(){
        hintWay[hintLevel++].SetActive(true);
        PlayerPrefs.SetInt("userHintCnt", --userHintCnt);
    }
}
