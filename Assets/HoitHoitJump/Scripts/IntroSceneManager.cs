﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour {
    void Start()
    {
        //StartCoroutine(GoToMainScene());
    }


    //IEnumerator GoToMainScene()
    //{
    //    yield return new WaitForSeconds(3);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    yield break;
    //}
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      
    }
}
