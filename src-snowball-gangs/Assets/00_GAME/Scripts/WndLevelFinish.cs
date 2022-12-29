using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndLevelFinish : MonoBehaviour
{
    [SerializeField] Button BtnMainMenu;
    [SerializeField] Button BtnNext;
    [SerializeField] Button BtnRestart;

    [SerializeField] GameObject PivotLoose;
    [SerializeField] GameObject PivotWin;


    public void SetWin(bool isWin)
    {
        PivotWin.SetActive(isWin);
        PivotLoose.SetActive(!isWin);
    }

    public void OnClickMainMenu()
    {
        Debug.Log("WndLevelFinish : OnClickMainMenu");
        CtrlGame.Instance.OnClickedMainMenu();
    }

    public void OnClickNext()
    {
        Debug.Log("WndLevelFinish : OnClickNext");
        CtrlGame.Instance.OnClickedNext();
    }

    public void OnClickRestart()
    {
        Debug.Log("WndLevelFinish : OnClickRestart");
        CtrlGame.Instance.OnClickedRestart();

    }

    private void AddListeners()
    {
        BtnMainMenu.onClick.AddListener(OnClickMainMenu);
        BtnNext.onClick.AddListener(OnClickNext);
        BtnRestart.onClick.AddListener(OnClickRestart);
    }

    private void Awake()
    {
        AddListeners();
    }
}
