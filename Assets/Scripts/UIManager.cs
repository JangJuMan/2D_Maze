using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Sample;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupHandler quitPopup;
    public PopupHandler hintPopup;
    public PopupHandler adPopup;
    public AdHandler adManager;

    public void UI_OpenQuitPopup(){
        quitPopup.Show();
    }
    public void UI_CloseQuitPopup(){
        quitPopup.Hide();
    }

    public void UI_OpenHintPopup(){
        Time.timeScale=0.0f;
        if(PlayerPrefs.GetInt("userHintCnt", 0) > 0){
            GameManager.Instance.SetUserHintCnt();
            hintPopup.Show();
        }
        else{
            adPopup.Show();
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
    public void UI_ViewAd(){
        adManager.LoadAd();
        adManager.ShowAd();
        Debug.Log("힌트개수 : before : " + PlayerPrefs.GetInt("userHintCnt"));
    }

}
