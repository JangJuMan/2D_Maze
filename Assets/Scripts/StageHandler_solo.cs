using UnityEngine;
using UnityEngine.UI;

public class StageHandler_solo : MonoBehaviour
{
    public GameObject[] levelObjs;
    private int currStage;

    void Start(){
        // 현재 진행된 레벨까지만 접근 가능하도록
        currStage = PlayerPrefs.GetInt("currStage", 1);
        for(int i=currStage; i<levelObjs.Length; i++){
            levelObjs[i].GetComponent<Image>().color = new Color32(119, 119, 119, 255);
            Destroy(levelObjs[i].GetComponent<Button>());
        }
    }


}
