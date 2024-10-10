using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseHandler : MonoBehaviour
{
    public PopupHandler popupWindow;

    public void ClosePopup(){
        popupWindow.Hide();
        Debug.Log("ClosePopup()");
    }
}
