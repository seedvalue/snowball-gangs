using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;

public class CtrlGame : MonoBehaviour
{
    public static CtrlGame Instance;
    
    [SerializeField] private bool TestAdd20Money = false;

    [SerializeField] private bool isTest = false;
    [SerializeField] private bool isOnlyPLayersSpawn = false;
    [SerializeField] private int playersCount = 1;
    
    

    private int curLevel =0;


    public int Restarts = 0;
    public int Dies = 0;



    #region SKIN

    public void SetSkinHuman(string skinName)
    {
        Debug.Log("CtrlGame : SetSkinHuman : " + skinName);
        PlayerPrefs.SetString(_skinPrefHuman, skinName);
        this.RefreshSkinsPrefs();
    }

    public void SetSkinCpu(string skinName)
    {
        Debug.Log("CtrlGame : SetSkinCpu : " + skinName);
        PlayerPrefs.SetString(_skinPrefCpu, skinName);
        this.RefreshSkinsPrefs();
    }

    [SerializeField]
    private string _skinHuman;
    [SerializeField]
    private string _skinCpu;

    private const string _skinPrefHuman = "SkinHuman";
    private const string _skinPrefCpu = "SkinCpu";

    private void RefreshSkinsPrefs()
    {
        if(PlayerPrefs.HasKey(_skinPrefHuman))
        {
            _skinHuman = PlayerPrefs.GetString(_skinPrefHuman);
        } else
        {
            _skinHuman = "Elf";
        }

        if (PlayerPrefs.HasKey(_skinPrefCpu))
        {
            _skinCpu = PlayerPrefs.GetString(_skinPrefCpu);
        }
        else
        {
            _skinCpu = "Santa";
        }
    }


    private string BuyedSkinPrefix = "BUYED_";

    public void BuySkinItem(string skinName, int price)
    {
        //minus money
        CurrentCandyValue -= price;
        if (CurrentCandyValue < 0) CurrentCandyValue = 0;
        PlayerPrefs.SetInt("CANDY", CurrentCandyValue);
        //save skin
        string resultPref = BuyedSkinPrefix + skinName;
        Debug.Log("Save pref = " + resultPref);
        PlayerPrefs.SetString(resultPref, "1");
        PlayerPrefs.Save();
    }

    public bool IsSkinBuyed(string skinName)
    {
        string resultPref = BuyedSkinPrefix + skinName;
        if (PlayerPrefs.HasKey(resultPref))
        {
            return true;
        }
        Debug.Log("CtrlGame : IsSkinBuyed : " + resultPref + " FALSE");
        return false;
    }


    #endregion 



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
        this.RefreshSkinsPrefs();
        int enemyCount = 1 + levelNum;
        int friendCount = 1 + levelNum / 2;
        CtrlCharSpawner.Instance.SpawnEnemies(enemyCount, _skinCpu);
        CtrlCharSpawner.Instance.SpawnPlayers(friendCount, _skinHuman);
        CtrlUi.Instance.ShowGamePlay();
    }

   

    private IEnumerator LoadLevelCo(int playerCount)
    {
        if(!isOnlyPLayersSpawn)
        {
            CtrlCharSpawner.Instance.SpawnEnemies(playerCount, _skinCpu);
        }
        
        CtrlCharSpawner.Instance.SpawnPlayers(playerCount, _skinHuman);
        yield return new WaitForSeconds(2F);
        
        //Идти вперед
    }



    public int CurrentCandyValue = 0;


    private void Add20Coins()
    {
        CurrentCandyValue += 20; 
        PlayerPrefs.SetInt("CANDY", CurrentCandyValue);
    }


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
        RefreshSkinsPrefs();
    }


    void Start()
    {
        Application.targetFrameRate = 60;
       // LoadLevel(1);

     

       if (isTest) LoadTest();
        CtrlUi.Instance._wndGamePlay.SetCandy(CurrentCandyValue);
    }

    private void Update()
    {
        if(TestAdd20Money)
        {
            Add20Coins();
            TestAdd20Money = false;
        }
    }
}


    

