using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterPageHandler : MonoBehaviour
{
    public SkinHandler skinHandler;
    public Text hintCnt;

    void Start(){
        hintCnt.text = PlayerPrefs.GetInt("userHintCnt", 0).ToString();
    }

    public void ChangeSkin(){
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        string skinName = obj.name;
        skinHandler.SetSkinByName(skinName);
        AudioHandler.Instance.PlaySfx(AudioHandler.Sfx.Pop);
    }
}
