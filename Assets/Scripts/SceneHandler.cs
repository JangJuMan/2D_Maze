using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public UIManager uiManger;

    void Awake(){
        Application.targetFrameRate = 120;
        //FOR DEBUG
        PlayerPrefs.SetInt("currStage", 11);
        PlayerPrefs.SetInt("userHintCnt", 12);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Get Active Scene : "+ SceneManager.GetActiveScene().name);
            AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
            switch(SceneManager.GetActiveScene().name){
                case "MainScene" :
                    uiManger.UI_OpenQuitPopup();
                    break;
                case "CharacterScene" :
                case "Stages_solo" :
                case "InGame_solo" :
                case "Settings":
                case "Explanation":
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
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }

    public void LoadCharacterScene(){
        SceneManager.LoadScene("CharacterScene");
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }

    public void LoadStages_solo(){
        SceneManager.LoadScene("Stages_solo");
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }

    public void LoadSettings(){
        SceneManager.LoadScene("Settings");
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }

    public void LoadSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
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
        // PlayerPrefs.DeleteAll();
    }
}
