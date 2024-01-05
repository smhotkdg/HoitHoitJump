using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public float distance = 3;
    public float velocity = 2;

    float angle = 0;


    bool bStart = false;
    void Update()
    {
        if (Time.timeScale != 1) return;
        
        if (GamePlayManager.Instance.bStartBuff == true)
        {
            if(bStart == false)
            {
                Vector2 Pos = this.transform.position;
                GameObject PlayerObj = GameObject.FindWithTag("Player").gameObject;
                Pos.x = PlayerObj.transform.position.x;
                this.transform.position = Pos;
                bStart = true;
            }    
        }
        else
        {
            
            if (bStart == true)
            {                
                //여기서 step을 원래 위치로 이동시켜야함
                if(prePos.x !=-1000 && prePos.y != -1000)
                {

                }
                else
                {
                    prePos = transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, prePos, 1f*Time.deltaTime);
                
                if (Mathf.Abs(transform.position.x) == Mathf.Abs(prePos.x))
                    bStart = false;
            }            
            if(bStart == false )
                MoveSideToSide();
        }
    }

    Vector3 prePos = new Vector3(-1000,-1000,0);
    void MoveSideToSide()
    {
        transform.position = new Vector2(Mathf.Sin(angle) * distance, transform.position.y);
        angle += velocity / 100;
        prePos = transform.position;
    }


    public void StartCoroutine_LandingEffect()
    {
        StartCoroutine(LandingEffect());
    }


    IEnumerator LandingEffect()
    {
        Vector2 originalPosition = transform.position;
        float originalPosition_Y = transform.position.y;
        float YChangeValue = 1.5f;

        while (YChangeValue > 0)
        {
            YChangeValue -= 0.1f;
            YChangeValue = Mathf.Clamp(YChangeValue, 0, 1.5f);
            transform.position = new Vector3(transform.position.x, originalPosition_Y - YChangeValue);
            yield return 0;
        }
        yield break;
    }


}

