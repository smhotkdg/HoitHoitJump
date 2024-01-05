using System;
using UnityEngine;
using GoogleMobileAds.Api;

using UnityEngine.UI;

public class AdsMobManager : MonoBehaviour
{
    public RewardBasedVideoAd rewardBasedVideo;
    public RewardBasedVideoAd BasedVideo;

    public InterstitialAd interstitial = null; // 전면광고 ;
    //Globalvariable gv;    


    public static AdsMobManager instance;
    private void Awake()
    {
        //gv = Globalvariable.Instance;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void _BannerShow()
    {
#if UNITY_ANDROID
        string AdUnitID = "ca-app-pub-7939215518934371/7856435066";

        //test
        //string AdUnitID = "ca-app-pub-3940256099942544/6300978111";
#else
        string AdUnitID = "unDefind";
#endif

        BannerView _banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Bottom);


        AdRequest request = new AdRequest.Builder()
        .AddExtra("max_ad_content_rating", "G")
        .Build();

        //***For Production When Submit App***
        //AdRequest request = new AdRequest.Builder().Build();

        BannerView bannerAd = new BannerView(AdUnitID, AdSize.SmartBanner, AdPosition.Bottom);

        _banner.LoadAd(request);
        _banner.Show();
        

    }
    

    
    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-7939215518934371~8061983077";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        //Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        Requestintersitial();

        interstitial.OnAdClosed += Interstitial_OnAdClosed;  // 이벤트 핸들러 추가 



        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;


        MobileAds.Initialize(appId);

        this.RequestRewardBasedVideo();


        //Get singleton reward based video ad reference.
        this.BasedVideo = RewardBasedVideoAd.Instance;
        

        MobileAds.Initialize(appId);

        this.RequestVideo();
        Requestintersitial();
        //if(gv.NoAds ==0)
        _BannerShow();

    }
    public void Requestintersitial()
    {
#if UNITY_ANDROID
        string AdUnitID = "ca-app-pub-7939215518934371/6340297120";

        //test
        //string AdUnitID = "	ca-app-pub-3940256099942544/8691691433";
#else
        string AdUnitID = "unDefind";
#endif

        interstitial = new InterstitialAd(AdUnitID);


        AdRequest request = new AdRequest.Builder()
        .AddExtra("max_ad_content_rating", "G")
        .Build();

        interstitial.LoadAd(request);        
        
    }
    public void Showintersitial()
    {
        if(interstitial.IsLoaded())
            interstitial.Show();// 전면 광고 출력
        Requestintersitial();
    }
    private void Interstitial_OnAdClosed(object sender, EventArgs e)
    {        

        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest request = new AdRequest.Builder()
      .AddExtra("max_ad_content_rating", "G")
      .Build();

        interstitial.LoadAd(request);// 전면 광고 요청

        GamePlayManager.Instance.bAds = false;
        GamePlayManager.Instance.index = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOverAds();
    }

    private void RequestVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7939215518934371/4603374047";


        //test
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-7939215518934371/8863564247";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest.Builder()
        .AddExtra("max_ad_content_rating", "G")
        .Build();


        // Load the rewarded video ad with the request.
        this.BasedVideo.LoadAd(request, adUnitId);

    }

    
    private void RequestRewardBasedVideo()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7939215518934371/4603374047";


        //test
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-7939215518934371/8863564247";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest.Builder()
        .AddExtra("max_ad_content_rating", "G")
        .Build();


        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);

    }
    public void LoadVideoAds()
    {
        RequestVideo();
    }
    public void LoadAd()
    {
        RequestRewardBasedVideo();
    }
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

        //text.text = "리워드 성공";
        this.RequestRewardBasedVideo();
        //gv.Gold += 30;
        //PlayerPrefs.SetInt("Gold", gv.Gold);
        //PlayerPrefs.Save();
        //SoundManager.instance.PlaySound("AdReward");
        //GameObject.Find("MainCanvas").GetComponent<UIManager>().SetGoldText();
        AdManager.instance.Reward();
    }
     

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
}
