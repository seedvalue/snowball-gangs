using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndMainMenu : MonoBehaviour
{
    public Button BtnPlay;
    public Button BtnSkinShop;




    public void OnclickedPlay()
    {
        Debug.Log("WndMainMenu : OnclickedPlay");
        CtrlUi.Instance.ShowGamePlay();
        CtrlGame.Instance.OnClickedPlay(0);
    }

    public void OnclickedSkinShop()
    {
        Debug.Log("WndMainMenu : OnclickedSkinShop");
        CtrlUi.Instance.ShowSkinShop();
    }

    private void Awake()
    {
        BtnPlay.onClick.AddListener(OnclickedPlay);
        BtnSkinShop.onClick.AddListener(OnclickedSkinShop);
    }
}
