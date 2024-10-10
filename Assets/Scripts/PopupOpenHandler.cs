using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOpenHandler : MonoBehaviour
{
    public PopupHandler popupWindow;

    public void OpenPopup(){
        Debug.Log("popupOpenHandler OpenPopup()");
        popupWindow.Show();
    }
}
