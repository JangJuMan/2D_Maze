using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.SceneManagement;

public class OutSideHandler : MonoBehaviour
{
    const string PLAYER = "Player";
    const string PLAYER2 = "Player2";
    const string FINISH = "Finish";
    const string FINISH2 = "Finish2";

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(PLAYER) 
        || other.gameObject.CompareTag(PLAYER2)
        || other.gameObject.CompareTag(FINISH)
        || other.gameObject.CompareTag(FINISH2)){
            SceneHandler.Instance.LoadSceneByName(SceneManager.GetActiveScene().name);
            AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Die);
        }
    }
}
