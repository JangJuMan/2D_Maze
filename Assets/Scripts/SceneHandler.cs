using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;
    public UIManager uiManger;

    void Awake(){
        Application.targetFrameRate = 120;
        //FOR DEBUG
        #if UNITY_EDITOR
            PlayerPrefs.SetInt("currStage", 15);
            PlayerPrefs.SetInt("userHintCnt", 12);
        #endif
    }

    void Start()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
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
                case "InGame_solo" :  // 현재 사용하지 않는 페이지
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
        #if UNITY_EDITOR    // FOR DEBUG
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Quit Application - Unity");
            PlayerPrefs.DeleteAll();
        #else
            Application.Quit(); // 어플리케이션 종료
            Debug.Log("Quit Application");
        #endif
    }
}
