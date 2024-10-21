using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintHandler : MonoBehaviour
{
    public GameObject[] hintWay;
    public Text hintCntTxt;

    int hintLevel = 0;
    int userHintCnt=0;


    void Start(){
        userHintCnt = PlayerPrefs.GetInt("userHintCnt", 3);
        Debug.Log("user Hint Cnt : "+ userHintCnt);
        Debug.Log("user Hint Cnt(변수) : "+ PlayerPrefs.GetInt("userHintCnt"));
        hintCntTxt.text = "(현재 보유 힌트 : " + userHintCnt +  "개)";
    }

    public void UseHint(){
        if(userHintCnt > 0){
            if(hintLevel < 3){
                hintWay[hintLevel++].SetActive(true);
            }
            PlayerPrefs.SetInt("userHintCnt", userHintCnt - 1);
        }
    }
}
