using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingHandler : MonoBehaviour
{
    public Button dragButton;
    public Button gyroButton;
    public Button btnButton;
    ColorBlock activeColorBlock;
    ColorBlock normalColorBlock;
    string currentActive;

    void Awake(){
        SetActiveColorBlock();
        SetNormalColorBlock();
        int mazeMoveType = PlayerPrefs.GetInt("mazeMoveType", 0);
        switch(mazeMoveType){
            case 0:  // 드래그 방식
                dragButton.colors = activeColorBlock;
                break;
            case 1:  // 자이로센서 방식
                gyroButton.colors = activeColorBlock;
                break;
            case 2:  // 버튼 방식
                btnButton.colors = activeColorBlock;
                break;
        }
    }

    void SetActiveColorBlock(){
        activeColorBlock.normalColor = new Color32(72, 29, 51, 255);
        activeColorBlock.highlightedColor = new Color32(164, 100, 132, 255);
        activeColorBlock.pressedColor = new Color32(149, 57, 103, 255);
        activeColorBlock.selectedColor = new Color32(72, 29, 51, 255);
        activeColorBlock.disabledColor = new Color32(120, 120, 120, 128);
        activeColorBlock.colorMultiplier = 1;
        activeColorBlock.fadeDuration = 0.1f;
    }

    void SetNormalColorBlock(){
        normalColorBlock.normalColor = new Color32(123, 61, 92, 255);
        normalColorBlock.highlightedColor = new Color32(164, 100, 132, 255);
        normalColorBlock.pressedColor = new Color32(149, 57, 103, 255);
        normalColorBlock.selectedColor = new Color32(72, 29, 51, 255);
        normalColorBlock.disabledColor = new Color32(120, 120, 120, 255);
        normalColorBlock.colorMultiplier = 1;
        normalColorBlock.fadeDuration = 0.1f;
    }

    public void ActivateDragType(){
        GameObject obj = EventSystem.current.currentSelectedGameObject;

        if(currentActive != obj.name){
            dragButton.colors = normalColorBlock;
            gyroButton.colors = normalColorBlock;
            btnButton.colors = normalColorBlock;

            obj.GetComponent<Button>().colors = activeColorBlock;

            switch(obj.name){
                case "DragBtn" :
                    PlayerPrefs.SetInt("mazeMoveType", 0);
                    break;
                case "GyroBtn" :
                    PlayerPrefs.SetInt("mazeMoveType", 1);
                    break;
                case "BtnBtn" :
                    PlayerPrefs.SetInt("mazeMoveType", 2);
                    break;
            }
        }
    }
}
