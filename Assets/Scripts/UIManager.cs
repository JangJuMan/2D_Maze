using GoogleMobileAds.Sample;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupHandler quitPopup;
    public PopupHandler hintPopup;
    public PopupHandler adPopup;
    public PopupHandler alertPopup;
    public PopupHandler noAdPopup;
    public AdHandler adManager;
    public GameObject leftBtn;
    public GameObject rightBtn;

    void Start(){
        if(leftBtn != null && rightBtn != null){
            if(PlayerPrefs.GetInt("mazeMoveType") == (int)MazeMoveHandler.MoveType.Btn){  
                leftBtn.SetActive(true);
                rightBtn.SetActive(true);
            }
            else{
                leftBtn.SetActive(false);
                rightBtn.SetActive(false);
            }
        }
    }

    public void UI_OpenQuitPopup(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Out);
        quitPopup.Show();
    }
    public void UI_CloseQuitPopup(){
        quitPopup.Hide();
    }

    public void UI_OpenHintPopup(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
        Time.timeScale=0.0f;
        
        switch(GameManager.Instance.HintCheck()){
            case GameManager.HintType.CanUseHint :
                hintPopup.Show();
                break;
            case GameManager.HintType.NoHint :
                adPopup.Show();
                break;
            case GameManager.HintType.AlreadyUsed:
                alertPopup.Show();
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
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop2);
        hintPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_CloseAdPopup(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop2);
        adPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_CloseAlertPopup(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop2);
        alertPopup.Hide();
        Time.timeScale = 1.0f;
    }
    public void UI_OpenNoAdPopup(){
        noAdPopup.Show();
        // 애니메이션 없이 adPopup 비활성화
        adPopup.transform.gameObject.SetActive(false);
    }
    public void UI_CloseNoAdPopup(){
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop2);
        // 애니메이션 없이 adPopup 활성화
        adPopup.transform.gameObject.SetActive(true);
        noAdPopup.Hide();
    }
    public void UI_ViewAd(){
        adManager.ShowAd();
    }
}
