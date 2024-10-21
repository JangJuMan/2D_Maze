using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupHandler quitPopup;
    public PopupHandler hintPopup;

    public void UI_OpenQuitPopup(){
        quitPopup.Show();
    }
    public void UI_CloseQuitPopup(){
        quitPopup.Hide();
    }

    public void UI_OpenHintPopup(){
        Time.timeScale=0.0f;
        GameManager.Instance.SetUserHintCnt();
        hintPopup.Show();
    }
    public void UI_UseHint(){
        GameManager.Instance.UseHint();
        UI_CloseHintPopup();
    }
    public void UI_CloseHintPopup(){
        hintPopup.Hide();
        Time.timeScale = 1.0f;
    }

}
