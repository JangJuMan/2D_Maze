using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PopupHandler popupWindow;

    public Text timeText;
    public Image[] starImages;
    public float[] stageGrade;

    float time = 0.0f;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    void Start(){
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }

    public void GameOver(){
        Time.timeScale = 0.0f;
        SetStars();
        popupWindow.Show();
    }

    private void SetStars(){
        for(int i=0; i<starImages.Length; i++){
            starImages[i].gameObject.SetActive(false);
        }

        if(time <= stageGrade[0]){
            starImages[0].gameObject.SetActive(true);
            starImages[1].gameObject.SetActive(true);
            starImages[2].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[1]){
            starImages[0].gameObject.SetActive(true);
            starImages[1].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
        else if(time <= stageGrade[2]){
            starImages[0].gameObject.SetActive(true);
            starImages[4].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
        else{
            starImages[3].gameObject.SetActive(true);
            starImages[4].gameObject.SetActive(true);
            starImages[5].gameObject.SetActive(true);
        }
    }
}
