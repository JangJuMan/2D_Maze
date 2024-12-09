using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLevels : MonoBehaviour
{
    Transform child;

    private const int FstarL = 0;
    private const int FstarR = 1;
    private const int FstarM = 2;
    private const int EstarL = 3;
    private const int EstarR = 4;
    private const int EstarM = 5;

    string currStageName;
    int currStageStars;

    void Start()
    {
        SetLevelStars();
    }

    void SetLevelStars(){
        currStageName = gameObject.name;
        currStageStars = PlayerPrefs.GetInt($"stageStars_{currStageName}", -1);
        child = gameObject.transform.GetChild(0);
        switch(currStageStars){
            case 0:
                child.GetChild(EstarL).gameObject.SetActive(true);
                child.GetChild(EstarR).gameObject.SetActive(true);
                child.GetChild(EstarM).gameObject.SetActive(true);
                break;
            case 1:
                child.GetChild(EstarL).gameObject.SetActive(true);
                child.GetChild(EstarR).gameObject.SetActive(true);
                child.GetChild(FstarM).gameObject.SetActive(true);
                break;
            case 2:
                child.GetChild(FstarL).gameObject.SetActive(true);
                child.GetChild(EstarR).gameObject.SetActive(true);
                child.GetChild(FstarM).gameObject.SetActive(true);
                break;
            case 3:
                child.GetChild(FstarL).gameObject.SetActive(true);
                child.GetChild(FstarR).gameObject.SetActive(true);
                child.GetChild(FstarM).gameObject.SetActive(true);
                break;
        }
    }
}
