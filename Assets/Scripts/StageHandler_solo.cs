using UnityEngine;
using UnityEngine.UI;

public class StageHandler_solo : MonoBehaviour
{
    public GameObject[] levelObjs;
    public ScrollRect scrollRect;
    
    private int currStage;


    void Start(){
        // 현재 진행된 레벨까지만 접근 가능하도록
        currStage = PlayerPrefs.GetInt("currStage", 1);
        for(int i=currStage; i<levelObjs.Length; i++){
            levelObjs[i].GetComponent<Image>().color = new Color32(119, 119, 119, 255);
            Destroy(levelObjs[i].GetComponent<Button>());
        }

        // 스크롤뷰 이전 위치 불러옴
        SetScrollRectPos();
    }

    public void SaveScrollRectPos(){
        MainController.Instance.SetScrollRectPos(scrollRect.content.anchoredPosition);
    }

    public void SetScrollRectPos(){
        scrollRect.content.anchoredPosition = MainController.Instance.GetScrollRectPos();
    }
}
