using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
        Debug.Log("Move to MainScene");
    }

    public void LoadCharacterScene(){
        SceneManager.LoadScene("CharacterScene");
        Debug.Log("Move to CharacterScene");
    }

    public void LoadSoloLevels(){
        SceneManager.LoadScene("SoloLevel");
        Debug.Log("Move to SoloPlayStage");
    }

    public void LoadGameStage(){
        SceneManager.LoadScene("SoloInGame1");
        Debug.Log("Move to SoloInGame1");
    }

    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Quit Application - Unity");
        #else
            Application.Quit(); // 어플리케이션 종료
            Debug.Log("Quit Application");
        #endif
    }
}
