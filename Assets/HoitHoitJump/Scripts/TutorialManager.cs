using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image MainImage;
    public Image TutorialImage1;
    public Image TutorialImage2;
 
    void Start()
    {
        //StartCoroutine(StartImage());
    }
    bool bChage = false;
    IEnumerator StartImage()
    {
        yield return new WaitForSeconds(0.25f);
        if(bChage ==false)
        {
            MainImage.sprite = TutorialImage1.sprite;
            bChage = true;
        }            
        else
        {
            MainImage.sprite = TutorialImage2.sprite;
            bChage = false;
        }
        StartCoroutine(StartImage());
    }
   
    void Update()
    {
        
    }
}
