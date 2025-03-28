using UnityEngine;

public class SkinBlocker : MonoBehaviour
{
    public int unLockLevel;

    void Start()
    {
        SetSkinBlocker(CharacterPageHandler.Instance.GetTotalStar());
    }

    private void SetSkinBlocker(int totalStar){
        if(totalStar >= unLockLevel){
            this.gameObject.SetActive(false);
        }   
    }


}
