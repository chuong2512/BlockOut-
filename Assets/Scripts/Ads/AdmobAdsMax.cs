using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AdmobAdsMax : MonoBehaviour
{
    public static AdmobAdsMax Instance;

    public static bool isAdmobInitDone;

#if UNITY_ANDROID
    private const string MaxSdkKey =
        "eQt0q3679KmUyKeNcSzqC01eB-lILmfTnJoufGxpSn__n1NVhHLeMgxZOaICke451El4ZBfuZum9Qw4WxzpW52";

    private const string InterstitialAdUnitId = "a5e4e9d0db1c4e7e";
    private const string RewardedAdUnitId = "6bbcff85917a54f8";
    private const string BanerAdUnitId = "5d5712ea055362d5";
    // #elif UNITY_IOS
    //     private const string MaxSdkKey =
    // "M4GLwqezVT2WDo75OWFGOV873pVg6-3S3Kpz8Rxe_-9CnHI9oXPB2TI5LpnRnqvr8hpH8kw7i4KTMcc891KCad";
    //     private const string InterstitialAdUnitId = "e62bc78796e8e537";
    //     private const string RewardedAdUnitId = "1ef401adad30de04";
    //     private const string BanerAdUnitId = "faa58304689e5d2b";
    // #endif
    // #if UNITY_ANDROID
    //     private string _adUnitIdHigh = "ca-app-pub-8467610367562059/9656627420";
    //     private string _adUnitIdMedium = "ca-app-pub-8467610367562059/2324932310";
    //     private string REWARD_INTER_ID = "ca-app-pub-8467610367562059/8893924685";
    // #elif UNITY_IPHONE
    //     private string _adUnitIdHigh = "ca-app-pub-3940256099942544/6978759866";
    //     private string _adUnitIdMedium = "ca-app-pub-3940256099942544/6978759866";
    //     private string REWARD_INTER_ID = "ca-app-pub-8467610367562059/4180939469";
    private string REWARD_INTER_ID = "test";
#else
  private string _adUnitId = "unused";
#endif
    public float countdownAds;
    bool isShowingAds;
    private bool _isInited;
    private IEnumerator reloadBannerCoru;
    public UnityAction actionInterstitialClose;
    private bool _isLoading;
    private UnityAction _actionClose;
    private UnityAction _actionRewardVideo;
    private UnityAction _actionNotLoadedVideo;
    private ActionWatchVideo actionWatchVideo;
    public Coroutine coroutineShowAppOpen;

    // private APSBannerAdRequest bannerAdRequest;
    // private const string SlotBannerId = "da89158c-1a1b-423d-bfd1-e5e2fbde42af";
    // private const string SlotIpadLeaderId = "aad4cd1a-4473-4ad0-90bb-6947d6c79e73";
    // private const string amazonAppId = "4fb1e5b9-4be3-4da2-9ab0-a5106875f7d4";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        Init();
    }

    public void Init()
    {
        //countdownAds = 0;

       

        _isInited = true;

        // //#if !UNITY_EDITOR
        List<string> testDeviceIds = new List<string>();

        testDeviceIds.Add("07276CEBDF187D3173080B9D8CF4FAB4");
        testDeviceIds.Add("5360A1B78DFA03B5BF8E964B5E27F91A");

      
    }

    //public IEnumerator ShowAppOpenAds()
    //{
    //    yield return new WaitUntil(() => GameController.Instance.progessLoading > 0.9f);
    //    if (SceneManager.GetActiveScene().name == SceneName.LOADING_SCENE)
    //    {
    //        AppOpenAdManager.Instance.ShowAdIfAvailable();
    //    }
    //}

    
    
    public enum ActionWatchVideo
    {
        None = 0,
        FreezeTime,
        BreakObject,
        Vacumn,
        UnlockScrewBox,
        AddTime
    }
}
