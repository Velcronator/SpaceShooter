using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class BannerAd : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // If the platform is not supported, _adUnitId will remain null
        if (_adUnitId != null)
        {
            LoadBannerAd();
        }
    }

    public void LoadBannerAd()
    {
        if(!Advertisement.isInitialized)
        {
            Debug.Log("Ads not initialized");
            return;
        }
        Advertisement.Banner.SetPosition(_bannerPosition);
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    private void OnBannerLoaded()
    {
        Advertisement.Banner.Show(_adUnitId);
    }
}
