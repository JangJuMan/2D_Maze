using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Sample;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupHandler quitPopup;
    public PopupHandler hintPopup;
    public PopupHandler adPopup;
    public PopupHandler alertPopup;
    public PopupHandler noAdPopup;
    public AdHandler adManager;

    public void UI_OpenQuitPopup(){
        quitPopup.Show();
    }
    public void UI_CloseQuitPopup(){
        quitPopup.Hide();
    }

    public void UI_OpenHintPopup(){
        Time.timeScale=0.0f;
        
        switch(GameManager.Instance.HintCheck()){
            case "CanUseHint" :
                hintPopup.Show();
                break;
            case "NoHint" :
                adPopup.Show();
                Debug.Log("사용 가능한 힌트가 없습니다");
                break;
            case "AlreadyUsed":
                alertPopup.Show();
                Debug.Log("해당 스테이지에서 힌트를 모두 사용하셨습니다");
                break;
            default :
                break;
        }
    }
    public void UI_UseHint(){
        GameManager.Instance.UseHint();
        UI_CloseHintPopup();
    }
    public void UI_CloseHintPopup(){
        hintPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_CloseAdPopup(){
        adPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_CloseAlertPopup(){
        alertPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_OpenNoAdPopup(){
        noAdPopup.Show();
        // 애니메이션 없이 adPopup 비활성화
        adPopup.transform.gameObject.SetActive(false);
        adPopup.popupBlocker.gameObject.SetActive(false);
    }
    public void UI_CloseNoAdPopup(){
        // 애니메이션 없이 adPopup 활성화
        adPopup.transform.gameObject.SetActive(true);
        adPopup.popupBlocker.gameObject.SetActive(true);
        noAdPopup.Hide();
    }
    public void UI_ViewAd(){
        adManager.ShowAd();
        Debug.Log("힌트개수 : before : " + PlayerPrefs.GetInt("userHintCnt"));
    }


}
