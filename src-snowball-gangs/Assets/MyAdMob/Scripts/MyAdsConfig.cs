
using UnityEngine;

[CreateAssetMenu]
public class MyAdsConfig : ScriptableObject
{
    [Tooltip("Will use test IDs verified by Google:")]
    public bool isTestingNow = false;
    

    public string idInterstitialAndroid;
    public string idInterstitialIphone;
    
    public string idRewardAndroid;
    public string idRewardIphone;
    
    public string idBannerAndroid;
    public string idBannerIphone;


   

    
}













[CreateAssetMenu]

public class MyAdsInerstitial : ScriptableObject
{
   
   
}

public class MyAdsBanner : ScriptableObject
{
    [SerializeField]
    private string Id = "ca-app-pub-7602426439989665/1364106346";
}