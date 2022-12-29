using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndPause : MonoBehaviour
{
    [SerializeField] private Button BtnResume;
    [SerializeField] private Button BtnReplay;
    [SerializeField] private Button BtnMainMenu;

    public void OnClickResume()
    {
        CtrlUi.Instance.ShowGamePlay();
    }

    public void OnClickReplay()
    {
        CtrlUi.Instance.ShowGamePlay();
        CtrlGame.Instance.OnClickedRestart();
    }

    public void OnClickMainMenu()
    {
        CtrlGame.Instance.OnClickedMainMenu();
    }

    private void AddListeners()
    {
        BtnResume.onClick.AddListener(OnClickResume);
        BtnReplay.onClick.AddListener(OnClickReplay);
        BtnMainMenu.onClick.AddListener(OnClickMainMenu);
    }

    private void Awake()
    {
        AddListeners();
    }
}
