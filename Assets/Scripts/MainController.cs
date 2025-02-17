using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance;

    Vector2 scrollRectPos;


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
}
