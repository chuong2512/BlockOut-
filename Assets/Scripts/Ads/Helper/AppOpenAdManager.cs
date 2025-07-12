// Copyright 2021 Google LLC
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AppOpenAdManager
{
    // #if UNITY_ANDROID
    //     private const string AD_UNIT_ID = "ca-app-pub-9457878244675693/3614647346";
    // #elif UNITY_IOS
    //     private const string AD_UNIT_ID = "ca-app-pub-9457878244675693/5619876469";
    // #else
#if UNITY_ANDROID
    private string AD_UNIT_ID = "ca-app-pub-8467610367562059/8314365051";
#elif UNITY_IPHONE
    string AD_UNIT_ID = "ca-app-pub-3940256099942544/5575463023";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

    private static AppOpenAdManager instance;

    private bool isShowingAd = false;

    // COMPLETE: Add loadTime field
    private DateTime loadTime;

    public static AppOpenAdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }

    public bool IsAdAvailable
    {
        get
        {
            // COMPLETE: Consider ad expiration
            return (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd(UnityAction actionLoadDone = null)
    {
   
    }

    public void ShowAdIfAvailable()
    {  
        if (UserData.IsRemoveAds)
        {
            return;
        }

        if (!IsAdAvailable || isShowingAd)
        {
            LoadAd();

            return;
        }

    }

    private void HandleAdDidDismissFullScreenContent()
    {
     
    }

 
}