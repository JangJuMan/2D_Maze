using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public UIManager uiManger;

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void FixedUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Get Active Scene : "+ SceneManager.GetActiveScene().name);
            switch(SceneManager.GetActiveScene().name){
                case "MainScene" :
                    uiManger.UI_OpenQuitPopup();
                    break;
                case "CharacterScene" :
                case "Stages_solo" :
                    LoadMainScene();
                    break;
                case "InGame_solo" :
                    LoadStages_solo();
                    break;
                case "Settings":
                    LoadMainScene();
                    break;
                default :  // 솔로 플레이의 모든 레벨에서
                    LoadStages_solo();
                    break;
            }
        }
    }

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
        SceneManager.LoadScene("Settings");
    }

    public void LoadInGame_solo(){
        SceneManager.LoadScene("InGame_solo");
    }

    public void LoadSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
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
