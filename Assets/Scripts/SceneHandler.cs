using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
    }

    public void LoadCharacterScene(){
        SceneManager.LoadScene("CharacterScene");
    }

    public void LoadStages_solo(){
        SceneManager.LoadScene("Stages_solo");
    }

    public void LoadStage_multi(){
        // TODO : Multi Play Stage 
    }

    public void LoadSettings(){
        // TODO : Setting  page
    }

    public void LoadInGame_solo(){
        SceneManager.LoadScene("InGame_solo");
    }

    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Quit Application - Unity");
        #else
            Application.Quit(); // 어플리케이션 종료
            Debug.Log("Quit Application");
        #endif
        // FOR DEBUG
        PlayerPrefs.DeleteAll();
    }
}
