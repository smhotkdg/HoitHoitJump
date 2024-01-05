using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    //public TextMeshProUGUI scoreText;

    //public TextMeshProUGUI bestValueText;
    //public TextMeshProUGUI bestText; 
    
    public GameObject SelectButton;
    public GameObject BuyButton;
    public GameObject CardInfoPanel;
    public List<GameObject> PlayerList;
    public GameObject GemObj;
    public GameObject BestScroeObj;
    public GameObject CurrentScoreObj;
    public GameObject Best;

    public GameObject FeverObj;
    public Text GemText;
    public Text scoreText;
    public Text scoreText2;
    public GameObject ScoreObj2;

    public Text bestValueText;
    public Text bestText;
    public Text ShopGemText;

    public GameObject ShopPanel;
    public GameObject GameOverPanel;
    public GameObject GameOverEffectPanel;

    public GameObject StartEffectPanel;

    public GameObject HowToPlayPanel;
    public MagneticScrollView.MagneticScrollRect magneticScroll;
    [HideInInspector]
    public bool isDead;

    public List<GameObject> GemList;
    public List<GameObject> BuyPlayerList;
    void InitShopPanel()
    {
        for(int i=1; i< GamePlayManager.Instance.OwnCharacterList.Count; i++)
        {
            if(GamePlayManager.Instance.OwnCharacterList[i] ==1)
            {
                GemList[i - 1].SetActive(false);
                BuyPlayerList[i - 1].SetActive(true);
            }
            else
            {
                if(GemList.Count <i-1)
                {
                    GemList[i - 1].SetActive(true);
                    BuyPlayerList[i - 1].SetActive(false);
                }
                
            }
        }
    }
    int score = 0;
    public void ShowShopPanel()
    {
        GamePlayManager.Instance.bSelectShop = true;
        Time.timeScale = 1f;
        ShopPanel.SetActive(true);
        InitShopPanel();
        ShopGemText.text = GamePlayManager.Instance.GetGem().ToString();
        GameOverPanel.SetActive(false);
        FeverObj.SetActive(false);
        GemObj.SetActive(false);
        BestScroeObj.SetActive(false);
        CurrentScoreObj.SetActive(false);
        Best.SetActive(false);
        ScoreObj2.SetActive(false);
    }
    public void SetCardInfoPanel(bool flag)
    {
        CardInfoPanel.SetActive(flag);
        if(flag == true)
        {
            //CardInfoPanel.GetComponent<GetCardInfo>().SetInit();
        }
    }
    public void BuyPlayer()
    {
        int index = -1;
        magneticScroll.GetSelected(out index);
        if(index >0)
        {
            int gem = index * 100;
            if(GamePlayManager.Instance.GetGem() >= gem)
            {
                GamePlayManager.Instance.SetGemCount(-gem);
                AddGem();
                GamePlayManager.Instance.AddCharacterList(index+1);
                InitShopPanel();
                ChangeCharictor();
                ShopGemText.text = GamePlayManager.Instance.GetGem().ToString();
            }
        }
    }
    public void ChangeCharictor()
    {
        int index = -1;
        magneticScroll.GetSelected(out index);
 
        if (GamePlayManager.Instance.OwnCharacterList[index] == 1)
        {
            SelectButton.SetActive(true);
            BuyButton.SetActive(false);
        }
        else
        {
            SelectButton.SetActive(false);
            BuyButton.SetActive(true);
        }
    }
    public void SelectPlayer()
    {
        int index = -1;
        magneticScroll.GetSelected(out index);        
        GamePlayManager.Instance.bSelectShop = false;
        Time.timeScale = 0f;
        ShopPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        
        GemObj.SetActive(true);
        BestScroeObj.SetActive(true);
        CurrentScoreObj.SetActive(true);
        Best.SetActive(true);
        ScoreObj2.SetActive(true);

        if (index >=0)
        {
            GamePlayManager.Instance.SetPlayerSelectNumber(index);
        }
    }

    void Awake()
    {
        isDead = false;
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        StartCoroutine(StartEffect());
        InitScore();        
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].SetActive(false);
        }
        PlayerList[GamePlayManager.Instance.PlayerSelectNumber].SetActive(true);
        GamePlayManager.Instance.bStartBuff = false;
    }


    IEnumerator StartEffect()
    {
        StartEffectPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        StartEffectPanel.SetActive(false);
        yield break;
    }

    
    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();

        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            bestValueText.text = score.ToString();
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    public void AddGem()
    {
        GemText.text = GamePlayManager.Instance.GetGem().ToString();
    }
    public void InitScore()
    {

        bestValueText.text = PlayerPrefs.GetInt("BestScore").ToString();
        GemText.text = GamePlayManager.Instance.GetGem().ToString();
    }

    public void GameOver()
    {

        //if(GamePlayManager.Instance.bAds == true)

//#if UNITY_EDITOR
//        GameOverAds();
//        return;
//#endif
        if (AdManager.instance.ShowFrontAds()==false)
        {
            GameOverAds();
        }
        
    }
    public void GameOverAds()
    {
        isDead = true;
        StartCoroutine(GameOverCoroutine());
    }
    IEnumerator GameOverCoroutine()
    {      
        Time.timeScale = 0.1f;
        GameOverEffectPanel.SetActive(true);
        scoreText.color = Color.white;
        scoreText2.color = Color.white;
        bestText.color = Color.gray;
        bestValueText.color = Color.gray;

        yield return new WaitForSecondsRealtime(0.5f);        
        Time.timeScale = 0.02f;
        GameOverPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(2.5f);
        if (GamePlayManager.Instance.bSelectShop == false)
        {
            Time.timeScale = 0f;
        }

        yield break;
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void OpenHowToPanel(){
        HowToPlayPanel.SetActive(true);
    } 
    
    public void CloseHowToPanel(){
         HowToPlayPanel.SetActive(false);
    }
    float timeLeft = 5f;

    void Update()
    {
        if(GamePlayManager.Instance.bStartBuff == true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;                
            }
            FeverObj.GetComponent<Text>().text = "Fever !!\n" + timeLeft.ToString("N2");
        }
    }
 
    public void StartBuff()
    {
        if(GamePlayManager.Instance.bStartBuff == false)
        {
            GamePlayManager.Instance.bStartBuff = true;
            FeverObj.SetActive(true);
            StartCoroutine(BuffStart());
        }
    }

    IEnumerator BuffStart()
    {
        yield return new WaitForSeconds(5);        
        FeverObj.SetActive(false);
        timeLeft = 5f;        
        GamePlayManager.Instance.bStartBuff = false;

    }
}
