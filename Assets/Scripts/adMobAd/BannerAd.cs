using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class BannerAd : MonoBehaviour
{
    private BannerView bannerView;
    private int adFree=0;
    // Start is called before the first frame update
    void Start()
    {
        adFree = PlayerPrefs.GetInt("adFree");

#if UNITY_ANDROID
        string appId = "ca-app-pub-3308520213502941~8071686351";
        //working id ca-app-pub-3308520213502941~2993081735
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";ca-app-pub-3940256099942544~3347511713
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.Show();

    }

    private void RequestBanner()
    {
        
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3308520213502941/3553200303";
        //working id ca-app-pub-3308520213502941/3135610015 ca-app-pub-3940256099942544/6300978111
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    private void Update()
    {
        if (adFree == 1)
        {
            bannerView.Destroy();
            bannerView.Hide();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bannerView.Hide();
            bannerView.Destroy();
        }
    }

    private void OnDisable()
    {
        bannerView.Hide();
        bannerView.Destroy();
    }



}