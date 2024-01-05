using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdManager : MonoBehaviour
{

    // Use this for initialization    

    //Globalvariable gv;
    public static AdManager instance;
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
    void Start()
    {
        
    }
    IEnumerator LoadAd()
    {        
        AdsMobManager.instance.LoadAd();
        yield return new WaitForSeconds(25);
        if (AdsMobManager.instance.rewardBasedVideo.IsLoaded() == false)
        {
            StartCoroutine(LoadAd());
        }
    }

    IEnumerator LoadAdVideo()
    {
        AdsMobManager.instance.LoadVideoAds();
        yield return new WaitForSeconds(25);
        if (AdsMobManager.instance.BasedVideo.IsLoaded() == false)
        {
            StartCoroutine(LoadAdVideo());
        }
    }

    IEnumerator LoadAdVideo2()
    {
        AdsMobManager.instance.Requestintersitial();
        yield return new WaitForSeconds(25);
        if (AdsMobManager.instance.interstitial.IsLoaded() == false)
        {
            StartCoroutine(LoadAdVideo2());
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }
    void Update()
    {

    }
    public void Reward()
    {
        switch (GamePlayManager.Instance.adstype)
        {
            case 5:
                GamePlayManager.Instance.SetGemCount(30);
                GameObject.Find("GameManager").GetComponent<GameManager>().AddGem();
                break;
        }
       
    }
    public bool ShowFrontAds()
    {
        UnityAdsHelper.instance.ShowPopup();
        return false;
        //AdsMobManager.instance.Showintersitial();
        //if (AdsMobManager.instance.interstitial.IsLoaded() == true)
        //    return true;
        //else
        //{
        //    UnityAdsHelper.instance.ShowPopup();
        //    StartCoroutine(LoadAdVideo2());
        //    return false;            
        //}
            
    }
    public void ShowVideo()
    {
        //if(gv.NoAds ==1)
        //{
        //    return;
        //}
        //UnityAdsObj.GetComponent<UnityAdsHelper>().ShowVideoAd();


        if (AdsMobManager.instance.BasedVideo.IsLoaded())
        {
            AdsMobManager.instance.BasedVideo.Show();
        }
        else
        {
            UnityAdsHelper.instance.ShowVideoAd();
            StartCoroutine(LoadAdVideo());
        }

    }
    public void ShowReward(int number)
    {

        GamePlayManager.Instance.adstype = number;
        if (AdsMobManager.instance.rewardBasedVideo.IsLoaded())
        {
            AdsMobManager.instance.rewardBasedVideo.Show();
        }
        else
        {            
            UnityAdsHelper.instance.ShowRewardedAd();
            StartCoroutine(LoadAd());
        }
    }
}

