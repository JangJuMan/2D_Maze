using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLevels : MonoBehaviour
{
    Transform child;

    private enum StageStars {FstarL, FstarR, FstarM, EstarL, EstarR, EstarM}

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
                child.GetChild((int)StageStars.EstarL).gameObject.SetActive(true);
                child.GetChild((int)StageStars.EstarR).gameObject.SetActive(true);
                child.GetChild((int)StageStars.EstarM).gameObject.SetActive(true);
                break;
            case 1:
                child.GetChild((int)StageStars.EstarL).gameObject.SetActive(true);
                child.GetChild((int)StageStars.EstarR).gameObject.SetActive(true);
                child.GetChild((int)StageStars.FstarM).gameObject.SetActive(true);
                break;
            case 2:
                child.GetChild((int)StageStars.FstarL).gameObject.SetActive(true);
                child.GetChild((int)StageStars.EstarR).gameObject.SetActive(true);
                child.GetChild((int)StageStars.FstarM).gameObject.SetActive(true);
                break;
            case 3:
                child.GetChild((int)StageStars.FstarL).gameObject.SetActive(true);
                child.GetChild((int)StageStars.FstarR).gameObject.SetActive(true);
                child.GetChild((int)StageStars.FstarM).gameObject.SetActive(true);
                break;
        }
    }
}
