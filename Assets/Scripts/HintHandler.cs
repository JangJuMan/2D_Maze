using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHandler : MonoBehaviour
{
    public GameObject[] hintWay;

    int currHint = 0;

    public void UseHint(){
        // TODO : 힌트 개수 체크
        // TODO : 힌트 개수 감소 
        if(currHint < 3){
            hintWay[currHint++].SetActive(true);
        }
    }
}
