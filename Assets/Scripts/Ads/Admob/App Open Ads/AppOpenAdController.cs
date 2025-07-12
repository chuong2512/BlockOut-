using System;
using UnityEngine;

/// <summary>
/// Demonstrates how to use the Google Mobile Ads app open ad format.
/// </summary>
[AddComponentMenu("GoogleMobileAds/Samples/AppOpenAdController")]
public class AppOpenAdController : MonoBehaviour
{

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/9257395921";
#elif UNITY_IPHONE
    string _adUnitId = "ca-app-pub-3940256099942544/5575463023";
#else
    private string _adUnitId = "unused";
#endif

    private DateTime _expireTime;

    public bool IsAdAvailable
    {
        get
        {
            return  DateTime.Now < _expireTime;
        }
    }

    private void Awake()
    {
       
    }

    private void OnDestroy()
    {
      //  AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    public void Start()
    {
     
    }

   
    public void LoadAppOpenAd()
    {
     
    }
}
