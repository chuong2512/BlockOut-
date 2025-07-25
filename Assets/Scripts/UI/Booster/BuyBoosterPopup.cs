using System;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class BuyBoosterPopup : BasePopup
{
    [SerializeField] private Button buyWithDiamondButton;
    [SerializeField] private Button getWithAdsButton;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private LeanLocalizedTextMeshProUGUI title;
    [SerializeField] private LeanLocalizedTextMeshProUGUI boosterDescription;

    [Header("BOOSTER SPRITE")]
    [SerializeField] private Sprite[] boosterSprites;
    [SerializeField] private string[] leanTranslationTitle;
    [SerializeField] private string[] leanTranslationNames;

    [Header("SCRIPTABLE OBJECT")]
    [SerializeField] private UserResourcesObserver userResourcesObserver;
    [SerializeField] private BoosterDataObserver boosterDataObserver;

    private BoosterType _boosterType;
    private bool _isBoughtBreakObjectBoosterByCoin;

    public static event Action<BoosterType> useBoosterEvent;
    public static event Action unlockScrewBoxEvent;
    public static event Action<ScreenRoute> openIAPShopPopupEvent;
    public static event Action<ScreenRoute> switchRouteEvent;
    public static event Action<int> updateBoosterQuantityEvent;
    public static event Action<float> updateCoinTextEvent;

    protected override void RegisterMoreEvent()
    {
        BoosterUI.showBuyBoosterPopupEvent += ShowBuyBoosterPopup;
        BoosterItemUI.showBuyBoosterPopupEvent += ShowBuyBoosterPopup;
        ScrewBoxUI.showBuyBoosterPopupEvent += ShowBuyBoosterPopup;
        BlockSelectionInput.breakObjectEvent += OnConfirmedBreakObject;
        BlockSelectionInput.vacumnEvent += OnConfirmedVacumn;

        buyWithDiamondButton.onClick.AddListener(GetWithCoin);
        getWithAdsButton.onClick.AddListener(GetWithAds);
    }

    protected override void UnregisterMoreEvent()
    {
        BoosterUI.showBuyBoosterPopupEvent -= ShowBuyBoosterPopup;
        BoosterItemUI.showBuyBoosterPopupEvent -= ShowBuyBoosterPopup;
        ScrewBoxUI.showBuyBoosterPopupEvent -= ShowBuyBoosterPopup;
        BlockSelectionInput.breakObjectEvent -= OnConfirmedBreakObject;
        BlockSelectionInput.vacumnEvent -= OnConfirmedVacumn;
    }

    private void ShowBuyBoosterPopup(BoosterType boosterType)
    {
        Show();

        _boosterType = boosterType;

        icon.sprite = boosterSprites[(int)_boosterType];

        icon.SetNativeSize();
        
        UIUtil.SetSizeKeepRatioY(icon, 0.35f * container.sizeDelta.x);

        costText.text = $"{boosterDataObserver.BoosterCosts[(int)_boosterType]}";
        title.TranslationName = leanTranslationTitle[(int)_boosterType];
        boosterDescription.TranslationName = leanTranslationNames[(int)_boosterType];

        // getWithAdsButton.onClick.RemoveAllListeners();

        // if (boosterType == BoosterType.UnlockScrewBox)
        // {
        //     getWithAdsButton.onClick.AddListener(UnlockScrewBoxWithAds);
        // }
        // else
        // {
        //     getWithAdsButton.onClick.AddListener(GetWithAds);
        // }
    }

    private void GetWithCoin()
    {
        userResourcesObserver.Load();

        int boosterIndex = (int)_boosterType;

        if (userResourcesObserver.UserResources.CoinQuantity >= boosterDataObserver.BoosterCosts[boosterIndex])
        {
            if (_boosterType == BoosterType.BreakObject || _boosterType == BoosterType.Vacumn)
            {
                userResourcesObserver.ChangeBoosterQuantity(boosterIndex, 1);

                updateBoosterQuantityEvent?.Invoke(boosterIndex);

                _isBoughtBreakObjectBoosterByCoin = true;
            }
            else
            {
                userResourcesObserver.ChangeCoin(-boosterDataObserver.BoosterCosts[boosterIndex]);
                userResourcesObserver.UserResources.BoosterQuantities[boosterIndex]++;

                updateCoinTextEvent?.Invoke(userResourcesObserver.UserResources.CoinQuantity);
            }

            userResourcesObserver.Save();

            Hide(onCompletedAction: () =>
            {
                useBoosterEvent?.Invoke(_boosterType);
            });
        }
        else
        {
            Hide(onCompletedAction: () =>
            {
                openIAPShopPopupEvent?.Invoke(ScreenRoute.Booster);
            });
        }
    }

    private void GetWithAds()
    {
        AdmobAdsMax.ActionWatchVideo actionWatchVideo;

        switch (_boosterType)
        {
            case BoosterType.FreezeTime: actionWatchVideo = AdmobAdsMax.ActionWatchVideo.FreezeTime; break;
            case BoosterType.BreakObject: actionWatchVideo = AdmobAdsMax.ActionWatchVideo.BreakObject; break;
            case BoosterType.Vacumn: actionWatchVideo = AdmobAdsMax.ActionWatchVideo.Vacumn; break;
            default: actionWatchVideo = AdmobAdsMax.ActionWatchVideo.FreezeTime; break;
        }

      //  AdmobAdsMax.Instance.ShowVideoReward(OnRewaredAdCompleted, actionNotLoadedVideo: ShowAdsNotLoadedPopup, actionClose: null, actionType: actionWatchVideo);
    }

    private void OnRewaredAdCompleted()
    {
        userResourcesObserver.ChangeBoosterQuantity((int)_boosterType, 1);

        Hide(onCompletedAction: () =>
        {
            useBoosterEvent?.Invoke(_boosterType);
        });
    }

    private void UnlockScrewBoxWithAds()
    {
        //AdmobAdsMax.Instance.ShowVideoReward(OnRewaredAdCompletedToUnlockBox, actionNotLoadedVideo: ShowAdsNotLoadedPopup, actionClose: null, actionType: AdmobAdsMax.ActionWatchVideo.UnlockScrewBox);
    }

    private void OnRewaredAdCompletedToUnlockBox()
    {
        Hide(onCompletedAction: () =>
        {
            unlockScrewBoxEvent?.Invoke();
        });
    }

    private void OnConfirmedBreakObject()
    {
        if (_isBoughtBreakObjectBoosterByCoin)
        {
            userResourcesObserver.ChangeCoin(-boosterDataObserver.BoosterCosts[(int)_boosterType]);

            updateCoinTextEvent?.Invoke(userResourcesObserver.UserResources.CoinQuantity);

            _isBoughtBreakObjectBoosterByCoin = false;
        }
    }

    private void OnConfirmedVacumn(GameFaction faction, Vector3 vacumnPosition)
    {
        if (_isBoughtBreakObjectBoosterByCoin)
        {
            userResourcesObserver.ChangeCoin(-boosterDataObserver.BoosterCosts[(int)_boosterType]);

            updateCoinTextEvent?.Invoke(userResourcesObserver.UserResources.CoinQuantity);

            _isBoughtBreakObjectBoosterByCoin = false;
        }
    }

    private void ShowAdsNotLoadedPopup()
    {
        switchRouteEvent?.Invoke(ScreenRoute.NoInternet);
    }
}
