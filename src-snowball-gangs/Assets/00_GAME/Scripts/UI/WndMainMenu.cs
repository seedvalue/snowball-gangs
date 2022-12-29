using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndMainMenu : MonoBehaviour
{
    public Button BtnPlay;





    public void OnclickedPlay()
    {
        Debug.Log("WndMainMenu : OnclickedPlay");
        CtrlUi.Instance.ShowGamePlay();
        CtrlGame.Instance.OnClickedPlay(0);
    }


    private void Awake()
    {
        BtnPlay.onClick.AddListener(OnclickedPlay);
    }
}
