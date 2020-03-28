using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class InterstitialAdScript : MonoBehaviour
{
    private MainScript ms;
    private int clevel;

private InterstitialAd interstitial;

    //request and show ad at start
    private void Start()
    {
        RequestInterstitial();
        ms = GetComponent<MainScript>();
       
    }

    private void RequestInterstitial()
{
        
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3308520213502941/6315095012";
        //the demo add"ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleInterstitialLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        //this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        string levelname = SceneManager.GetActiveScene().name;// Application.loadedLevelName;
        clevel = int.Parse(levelname.Substring(5, levelname.Length - 5));
        
        SceneManager.LoadScene("level" + (clevel + 1));
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
       
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


    public void showAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
       
    }

}
