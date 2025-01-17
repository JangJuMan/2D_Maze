using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SkinHandler : MonoBehaviour
{
    private Image img;
    private string skinName;
    private const string defaultSkin = "character_0";

    void Start(){
        img = GetComponent<Image>();
        skinName = PlayerPrefs.GetString("skinName", defaultSkin);
        SetSkinByName(skinName);
    }

    public void SetSkinByIdx(int idx){
        img.sprite = Resources.Load<Sprite>($"Images/character_{idx}");
    }

    public void SetSkinByName(string imgName){
        img.sprite = Resources.Load<Sprite>($"Images/{imgName}");
        PlayerPrefs.SetString("skinName", imgName);
    }
}
