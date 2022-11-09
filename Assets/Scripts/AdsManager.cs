using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] string androidPlacementId;
    [SerializeField] string iOSPlacementId;
    [SerializeField] bool testMode;
    private string gameId;
    private string placementId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
                gameId = iOSGameId;
                placementId = iOSPlacementId;
                break;

            case RuntimePlatform.Android:
                gameId = androidGameId;
                placementId = androidPlacementId;
                break;

            case RuntimePlatform.WindowsEditor:
                gameId = androidGameId;
                placementId = androidPlacementId;
                break;
        }

        Advertisement.Initialize(gameId, false, this);
    }

    public void LoadAd()
    {
        Advertisement.Load(placementId, this);
    }

    public void ShowAdd()
    {
        Advertisement.Show(placementId, this);
        LoadAd();
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId){ }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}
