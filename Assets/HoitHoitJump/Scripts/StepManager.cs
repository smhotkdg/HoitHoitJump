using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{

    //public GameObject StepPrefab;
    public List<GameObject> StepPrefabList;

    [Space]
    public float stepWidth;
    public float stepHeight;
    [Space]
    public float decreasStepWidth;
    public float minimumStepWidth;

    public int NumberOfStartSteps = 4;
    public int DistanceToNextStep = 6;
    public GameObject Gem;
    public GameObject BuffGem;
    int stepIndex = 0;

    float halfWidth;


    void Start()
    {
        halfWidth = GameObject.Find("GameManager").GetComponent<GetDisplayBound>().Right;
        InitSteps();
    }

    void InitSteps()
    {
        for (int i = 0; i < NumberOfStartSteps; i++)
        {          
            MakeStep();           
            
        }
    }

    int StepCount = 0;
    public void MakeStep()
    {
        int randomPosx;
        if (stepIndex == 0)
            randomPosx = 0;
        else
            randomPosx = Random.Range(-4, 5);

        Vector2 pos = new Vector2(randomPosx, stepIndex * DistanceToNextStep);
        
        int index = 0;
        if(StepCount / 3 >= StepPrefabList.Count)
        {
            index = StepPrefabList.Count - 1;
        }
        else
        {
            index = StepCount / 3;
        }
        //int randPrefab = Random.Range(0, StepPrefabList.Count);
        GameObject stepObj = Instantiate(StepPrefabList[index], pos, Quaternion.identity);
        stepObj.transform.SetParent(transform);
        stepObj.transform.localScale = new Vector2(stepWidth, stepHeight);

        int randGem = Random.Range(0, 1);
        if(randGem ==0)
        {
            if (StepCount > 0)
            {
                int randBuffGem = Random.Range(0, 10);
                if (randBuffGem == 0)
                {
                    Vector2 Gempos = new Vector2(randomPosx, stepIndex * DistanceToNextStep + 1);
                    GameObject GemObj = Instantiate(BuffGem, Gempos, Quaternion.identity);
                    GemObj.transform.SetParent(stepObj.transform);
                }
                else
                {
                    Vector2 Gempos = new Vector2(randomPosx, stepIndex * DistanceToNextStep + 1);
                    GameObject GemObj = Instantiate(Gem, Gempos, Quaternion.identity);
                    GemObj.transform.SetParent(stepObj.transform);
                }

            }

        }

        DecreaseStepWidth();        
        SetSpeed(stepObj);
        SetDirection(stepObj);        
        
        IncreaseStepIndex();
        StepCount++;
    }


    void SetSpeed(GameObject newStepObj)
    {
        newStepObj.GetComponent<Step>().distance = Random.Range(1, halfWidth);
        if (stepIndex == 0 || GamePlayManager.Instance.isTutorial ==0)
            newStepObj.GetComponent<Step>().velocity = 0;
        else
            newStepObj.GetComponent<Step>().velocity = Random.Range(3, 5);
    }

    void SetDirection(GameObject newStepObj)
    {
        if (Random.Range(0, 2) == 0)
        {
            newStepObj.GetComponent<Step>().velocity *= -1;
        }
    }

    void IncreaseStepIndex()
    {
        stepIndex++;
    }

    void DecreaseStepWidth()
    {
        if (stepWidth > minimumStepWidth)
        {
            stepWidth -= decreasStepWidth;
        }
    }

   
}
