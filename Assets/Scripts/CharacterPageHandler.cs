using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterPageHandler : MonoBehaviour
{
    public static CharacterPageHandler Instance;
    public SkinHandler skinHandler;
    public Text starCnt;

    private int totalStar = -1;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        SetStarCnt();
    }

    private void SetStarCnt(){
        totalStar = GetTotalStar();
        starCnt.text = totalStar.ToString();
    }

    public int GetTotalStar(){
        if(totalStar != -1){
            return totalStar;
        }
        
        // 최초 1회만 계산
        int starCnt = 0;
        for(int stage=1; stage<=MainController.Instance.GetMaxStage(); stage++){
            for(int level=1; level<=MainController.Instance.GetMaxLevel(); level++){
                starCnt += PlayerPrefs.GetInt($"stageStars_Level{stage}_{level}", 0);
            }
        }
        return starCnt;
    }

    public void ChangeSkin(){
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        string skinName = obj.name;
        skinHandler.SetSkinByName(skinName);
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }
}
