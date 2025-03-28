using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance;

    Vector2 scrollRectPos;
    const int _maxStage = 5; // 총 스테이지 수
    const int _maxLevel = 3; // 스테이지 당 레벨 수


    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SetScrollRectPos(Vector2 vec){
        scrollRectPos = vec;
    }

    public Vector2 GetScrollRectPos(){
        return scrollRectPos;
    }

    public int GetMaxStage(){
        return _maxStage;
    }
    public int GetMaxLevel(){
        return _maxLevel;
    }
}
