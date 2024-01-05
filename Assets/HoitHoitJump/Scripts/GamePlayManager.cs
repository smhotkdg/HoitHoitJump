using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance = null;
    public GameObject TutorialManager;
    public int isTutorial;
    public static GamePlayManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitData();
    }
    int GemCount;
    int CurrentScore = 0;
    public bool bStartBuff = false;
    public bool bSelectShop = false;
    public int PlayerSelectNumber = 0;
    public int BGSelectNumber = -1;
    public int adstype = 0;
    public List<int> OwnCharacterList = new List<int>();
    public void ShowTutorial(bool flag)
    {
        if (TutorialManager == null)
            return;
        TutorialManager.SetActive(flag);
    }
    public int index = 0;
    public bool bAds = false;
    public void AddCurrentScore(int index)
    {
        CurrentScore = CurrentScore + index;
        index++;
        int rand = Random.Range(10, 30);
        if(index >rand)
        {
            bAds = true;
        }
    }
    public void SetZeroCurrentScore()
    {
        CurrentScore = 0;
    }
    public void SetPlayerSelectNumber(int index)
    {
        string temp = "PlayerSelectNumber";
        PlayerSelectNumber = index;
        PlayerPrefs.SetInt(temp, PlayerSelectNumber);
        PlayerPrefs.Save();
    }
    public int GetCurrentScore()
    {
        return CurrentScore;
    }
    void InitData()
    {
        GemCount = PlayerPrefs.GetInt("Gem");
        isTutorial = PlayerPrefs.GetInt("Tutorial");
        for (int i=0; i< 5; i++)
        {
            int index = i + 1;
            string temp = "OwnCharacter" + index;
            OwnCharacterList.Add(0);
            OwnCharacterList[i] = PlayerPrefs.GetInt(temp);
        }
        OwnCharacterList[0] = 1;
        string strPlayerSelectNumber = "PlayerSelectNumber";
        PlayerSelectNumber =  PlayerPrefs.GetInt(strPlayerSelectNumber);
        if (isTutorial == 0)
            ShowTutorial(true);
    }
    public void SaveTutorial()
    {
        isTutorial = 1;
        PlayerPrefs.SetInt("Tutorial",isTutorial);
        PlayerPrefs.Save();
    }
    public void AddCharacterList(int index)
    {
        string temp = "OwnCharacter" + index;
        OwnCharacterList[index - 1] = 1;
        PlayerPrefs.SetInt(temp, OwnCharacterList[index-1]);
        PlayerPrefs.Save();
    }
    public void SetGemCount(int i)
    {
        GemCount = GemCount + i;
        PlayerPrefs.SetInt("Gem", GemCount);
        PlayerPrefs.Save();
    }
    public int GetGem()
    {
        return GemCount;
    }
    
}
