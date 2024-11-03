using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    [SerializeField] BannerAd _bannerAd;

    [SerializeField] int _timeToSkip = 1;

    string _adUnitId;

    void Awake()
    {
        //// Get the Ad Unit ID for the current platform:
        //_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
        //    ? _iOsAdUnitId
        //    : _androidAdUnitId;

        // Get the Ad Unit ID for the current platform: Only for testing in the editor for iOS
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        int skipNumber = PlayerPrefs.GetInt("InterstitialSkipNumber", _timeToSkip);
        if (skipNumber == 0)
        {
            LoadAd();
            PlayerPrefs.SetInt("InterstitialSkipNumber", _timeToSkip);
        }
        else
        {
            PlayerPrefs.SetInt("InterstitialSkipNumber", skipNumber - 1);
        }
        // If the platform is not supported, _adUnitId will remain null
        if (_adUnitId != null)
        {
            LoadAd();
        }
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if(!Advertisement.isInitialized)
        {
            Debug.Log("Ads not initialized");
            return;
        }
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        Debug.Log("Ad Loaded: " + adUnitId);
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId) 
    { 
        Debug.Log("Ad started: " + _adUnitId);
        Advertisement.Banner.Hide();
        Time.timeScale = 0;
    }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    { 
        Debug.Log("Ad completed: " + _adUnitId);
        Time.timeScale = 1;
        _bannerAd.LoadBannerAd();
    }
}