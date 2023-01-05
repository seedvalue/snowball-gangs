using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;

public class CtrlGame : MonoBehaviour
{
    public static CtrlGame Instance;

    [SerializeField] private bool isTest = false;
    [SerializeField] private bool isOnlyPLayersSpawn = false;
    [SerializeField] private int playersCount = 1;
    
    

    private int curLevel =0;


    public int Restarts = 0;
    public int Dies = 0;


    public void OnSomeWin(int livePlayers, int liveEnemies)
    {
        if(livePlayers > liveEnemies)
        {
            CtrlUi.Instance.ShowLevelFinish(true);
        } else
        {
            CtrlUi.Instance.ShowLevelFinish(false);
            Dies++;
            if (Dies >= 3)
            {
                //ShowAds();
                Dies = 0;
            }

        }

        if(curLevel >= 1)
        {
            ShowAds();
        }
    }


    private void ShowAds()
    {
        StartCoroutine(ShowAdsCo());
    }

    private IEnumerator ShowAdsCo()
    {
        CtrlUi.Instance.ShowLoading(false);
        if (CtrlAds.Instance.IsHaveAds())
        {
            CtrlUi.Instance.ShowLoading(true);
            yield return new WaitForSeconds(1F);
            CtrlUi.Instance.ShowLoading(false);
            CtrlAds.Instance.ShowInterstitial();
        } else
        {
            CtrlUi.Instance.ShowLoading(false);
            CtrlAds.Instance.RequestInterstitial();
            Debug.LogError("CtrlGame : ShowAdsCo : CtrlAds.Instance.IsHaveAds() == FALSE ");
        }
    }


    public void OnClickedPlay(int levelNum)
    {
        CtrlCharactersData.Instance.CleanAll();
        LoadLevel(levelNum);
    }

    public void OnClickedRestart()
    {
        //Destroy Old players
        CtrlCharactersData.Instance.CleanAll();
        LoadLevel(curLevel);
    }

    public void OnClickedNext()
    {
        //Destroy Old players
        CtrlCharactersData.Instance.CleanAll();
        curLevel++;
        LoadLevel(curLevel);
    }

    public void OnClickedMainMenu()
    {
        //Destroy Old players
        CtrlCharactersData.Instance.CleanAll();
        curLevel = 0;
        CtrlUi.Instance.ShowMainMenu();
    }


    private void LoadTest()
    {
        StartCoroutine(LoadLevelCo(playersCount));
    }
    
    

    public void LoadLevel(int levelNum)
    {
        int enemyCount = 1 + levelNum;
        int friendCount = 1 + levelNum / 2;
        CtrlCharSpawner.Instance.SpawnEnemies(enemyCount);
        CtrlCharSpawner.Instance.SpawnPlayers(friendCount);
        CtrlUi.Instance.ShowGamePlay();
    }

   

    private IEnumerator LoadLevelCo(int playerCount)
    {
        if(!isOnlyPLayersSpawn)
        {
            CtrlCharSpawner.Instance.SpawnEnemies(playerCount);
        }
        
        CtrlCharSpawner.Instance.SpawnPlayers(playerCount);
        yield return new WaitForSeconds(2F);
        
        //Идти вперед
    }



    public int CurrentCandyValue = 0;

    public void OnCandyGet()
    {
        CurrentCandyValue++;
        PlayerPrefs.SetInt("CANDY", CurrentCandyValue);
        CtrlUi.Instance._wndGamePlay.SetCandy(CurrentCandyValue);
        CtrlSound.Instance.PlayCandyGet();
    }


    public void OnCandyGetEnemy()
    {
        CurrentCandyValue--;
        if (CurrentCandyValue < 0) CurrentCandyValue = 0;
        PlayerPrefs.SetInt("CANDY", CurrentCandyValue);
        CtrlUi.Instance._wndGamePlay.SetCandy(CurrentCandyValue);
        CtrlSound.Instance.PlayCandyLooseUi();
    }



    private void Awake()
    {
        Instance = this;
        CurrentCandyValue = PlayerPrefs.GetInt("CANDY");
    }


    void Start()
    {
        Application.targetFrameRate = 60;
       // LoadLevel(1);

     

       if (isTest) LoadTest();
        CtrlUi.Instance._wndGamePlay.SetCandy(CurrentCandyValue);
    }
}


    

