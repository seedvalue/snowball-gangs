//MyAdmob 001 2022.11.22

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using GoogleMobileAds.Api;

public class CtrlAds : MonoBehaviour
{
	[SerializeField] private MyAdsConfig config;

	[Header("Keep between scenes:"), Tooltip("Recommend enabled to keep in all scenes"), SerializeField]
	private bool dontDestroyOnLoad = true; 	
	[Header("InstanceDebugUi:"), Tooltip("Show ads logging in Ui"), SerializeField]
	private bool isShowDebugUi = true;
	public static CtrlAds Instance;




	#region DebugUi

	private void ShowDebugUi(bool isShow)
	{
		if (!isShow) return;
		
	}
	

	#endregion
	
	
	

	#region DEFAULT TESTING UNITS ID

	//TEST VALUES from https://developers.google.com/admob/unity/rewarded-ads
	private const string IDRewardedAndroidTest = "ca-app-pub-3940256099942544/5224354917";
	private const string IDRewardedIphoneTest = "ca-app-pub-3940256099942544/1712485313";
	//TEST VALUES from https://developers.google.com/admob/unity/interstitial
	private const string IDInterstitialAndroidTest = "ca-app-pub-3940256099942544/1033173712";
	private const string IDInterstitialIphoneTest = "ca-app-pub-3940256099942544/4411468910";
	//TEST VALUES from https://developers.google.com/admob/unity/interstitial
	private const string IDBannerAndroidTest = "ca-app-pub-3940256099942544/6300978111";
	private const string IDBannerIphoneTest = "ca-app-pub-3940256099942544/2934735716";
	private const string IDUnexpected = "unexpected_platform";

	#endregion
	
	private BannerView _bannerView;
	private AdRequest _reqBanner;
	private InterstitialAd _interstitial;
	private AdRequest _reqInterstitial;
	private RewardedAd _rewardedAd;
	
	#region REQUEST BANNER INTERSITIAL REWARD

	public void RequestBanner ()
	{
		string adUnitId = GetUnitId(MyAdsType.Banner);
		// Create a 320x50 banner at the top of the screen.
		_bannerView = new BannerView (config.idBannerAndroid, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		_reqBanner = new AdRequest.Builder ().Build ();
		// Load the banner with the request.
		_bannerView.LoadAd (_reqBanner);
	}
	
	private void RequestInterstitial()
	{
		string adUnitId = GetUnitId(MyAdsType.Interstitial);
		// Initialize an InterstitialAd.
		this._interstitial = new InterstitialAd(adUnitId);
		// Called when an ad request has successfully loaded.
		this._interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this._interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this._interstitial.OnAdOpening += HandleOnAdOpening;
		// Called when the ad is closed.
		this._interstitial.OnAdClosed += HandleOnAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this._interstitial.LoadAd(request);
	}
	
	private void RequestRewarded()
	{
		string adUnitId = GetUnitId(MyAdsType.Rewarded);
		// Initialize an RewardedAd.
		this._rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this._rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this._rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this._rewardedAd.LoadAd(request);
	}
	
	/*
	private NativeAd nativeAd;

	private void RequestNativeAd() {
		string adUnitId = GetUnitId(MyAdsType.Native);
		AdLoader adLoader = new AdLoader.Builder(adUnitId)
			.ForNativeAd()
			.Build();
		adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
		adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		adLoader.LoadAd(new AdRequest.Builder().Build());
	}
	*/
	
	#endregion
	
	
	#region HANDLE REWARDED

	private void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: " + args.LoadAdError);
	}

	private void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
			+ args.AdError);
	}

	private void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
	}

	private void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
			$"HandleRewardedAdRewarded event received for {amount} {type}");
	}

	#endregion
	
	
	#region HANDLE INTERSTITIAL

	
	

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
		                    + args.LoadAdError);
		this.RequestInterstitial();
	}

	public void HandleOnAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpening event received");
		this.RequestInterstitial();
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		this.RequestInterstitial();
	}


	#endregion


	#region PUBLIC SHOW

	public void ShowRewarded()
	{
		Debug.Log("CtrlAds : ShowRewarded");
		if (this._rewardedAd.IsLoaded()) {
			this._rewardedAd.Show();
		}
		else
		{
			Debug.LogError("CtrlAds : ShowRewarded : _rewardedAd.IsLoaded IS FALSE");
		}
	}
	public void ShowInterstitial ()
	{
		Debug.Log("CtrlAds : ShowInterstitial");
		if (_interstitial.IsLoaded ()) {
			_interstitial.Show ();
		} else {
			Debug.LogError("ShowInterstitial : _interstitial.IsLoaded is FALSE");
		}
	}




	#endregion


	#region INIT

	private void Init()
	{
		if (CtrlAds.Instance) Destroy(gameObject);
		else
		{
			ShowDebugUi(isShowDebugUi);
			// Initialize the Google Mobile Ads SDK.
			RequestConfiguration requestConfiguration =
				new RequestConfiguration.Builder()
					.SetSameAppKeyEnabled(true).build();
			MobileAds.SetRequestConfiguration(requestConfiguration);
			// Initialize the Google Mobile Ads SDK.
			MobileAds.Initialize(HandleInitCompleteAction);

			//MobileAds.Initialize(initStatus => { });
			Instance = this;
			if(dontDestroyOnLoad)DontDestroyOnLoadThis();
		}
	}

	private void DontDestroyOnLoadThis()
	{
		transform.SetParent(null);
		DontDestroyOnLoad(this);
	}
	
	private void HandleInitCompleteAction(InitializationStatus obj)
	{
		Debug.Log("CtrlAds : HandleInitCompleteAction");
	}


	#endregion
	
	private string GetUnitId(MyAdsType adsType)
	{
		string adUnitId = IDUnexpected;
		RuntimePlatform platform = Application.platform;

		switch (adsType)
		{
			case MyAdsType.None:
				Debug.LogError("CtrlAds : GetUnitId : MyAdsType.None");
				break;
			case MyAdsType.Banner:
				if(platform == RuntimePlatform.Android) 
					adUnitId = config.isTestingNow ? IDBannerAndroidTest : config.idBannerAndroid;
				if(platform == RuntimePlatform.IPhonePlayer) 
					adUnitId = config.isTestingNow ? IDBannerIphoneTest : config.idBannerIphone;
				break;
			case MyAdsType.Interstitial:
				if(platform == RuntimePlatform.Android) 
					adUnitId = config.isTestingNow ? IDInterstitialAndroidTest : config.idInterstitialAndroid;
				if(platform == RuntimePlatform.IPhonePlayer) 
					adUnitId = config.isTestingNow ? IDInterstitialIphoneTest : config.idInterstitialIphone;
				break;
			case MyAdsType.Native:
				Debug.LogError("CtrlAds : GetUnitId : MyAdsType.Native NOT IMPLEMENTED"); //TODO NATIVE
				break;
			case MyAdsType.Rewarded:
				if(platform == RuntimePlatform.Android) 
					adUnitId = config.isTestingNow ? IDRewardedAndroidTest : config.idRewardAndroid;
				if(platform == RuntimePlatform.IPhonePlayer) 
					adUnitId = config.isTestingNow ? IDRewardedIphoneTest : config.idRewardIphone;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(adsType), adsType, null);
		}
		return adUnitId;
	}

	#region UNITY

	void Awake ()
	{
		Init();
	}

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		RequestInterstitial ();
		//RequestBanner();
		//RequestRewarded();


		InvokeRepeating("RequestInterstitial", 5, 10);
	}

	#endregion
	
}

public class MyAdsDetails
{

}

internal enum MyAdsState
{
	None,
	Requested,
	Downloaded,
	Showed
}

internal enum MyAdsType
{
	None,
	Banner,
	Interstitial,
	Native,
	Rewarded
}

