using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlUi : MonoBehaviour
{
    public static CtrlUi Instance;


    [SerializeField] private WndMainMenu _wndMainMenu;
    [SerializeField] public WndGamePlay _wndGamePlay;
    [SerializeField] private WndPause _wndPause;
    [SerializeField] private WndLevelFinish _wndFinishLevel;
    [SerializeField] private WndSkinShop _wndSkinShop;

    [SerializeField] private GameObject _wndLoading;

    public bool isGamePlayTest = false;

    public void ShowSkinShop()
    {
        HideAll();
        _wndSkinShop.gameObject.SetActive(true);
        CtrlSound.Instance.PlaySkinShop();
    }


    public void ShowLoading(bool isShow)
    {
        _wndLoading.SetActive(isShow);
    }

    public void ShowLevelFinish(bool isWin)
    {
        HideAll();
        _wndFinishLevel.gameObject.SetActive(true);
        _wndFinishLevel.SetWin(isWin);
        CtrlSound.Instance.PlayLevelFinish(isWin);
    }


    public void ShowMainMenu()
    {
        HideAll();
        _wndMainMenu.gameObject.SetActive(true);
        CtrlSound.Instance.PlayMainMenu();
    }

    public void ShowGamePlay()
    {
        HideAll();
        _wndGamePlay.gameObject.SetActive(true);
        CtrlSound.Instance.PlayGamePlay();
    }

    public void ShowPause()
    {
        HideAll();
        _wndPause.gameObject.SetActive(true);
        Time.timeScale = 0F;
    }







    private void HideAll()
    {
        Time.timeScale = 1F;
        if(_wndMainMenu) _wndMainMenu.gameObject.SetActive(false);
        if (_wndGamePlay) _wndGamePlay.gameObject.SetActive(false);
        if (_wndPause) _wndPause.gameObject.SetActive(false);
        if (_wndFinishLevel) _wndFinishLevel.gameObject.SetActive(false);
        if (_wndSkinShop) _wndSkinShop.gameObject.SetActive(false);
    }




    private void Awake()
    {
        Instance = this;
        ShowLoading(false);
    }

    private void Start()
    {
        if (isGamePlayTest) ShowGamePlay();
        else ShowMainMenu();
        
    }
}
