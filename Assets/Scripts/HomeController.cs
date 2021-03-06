﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeController : MonoBehaviour {

    void Start()
    {
    
        if (PlayerPrefs.HasKey("FirstApple"))
        {
            PlayerPrefs.DeleteKey("FirstApple");
            PlayerPrefs.Save();
        }


        Screen.fullScreen = true;
    }
    public void BtCredits()
    {
        SceneManager.LoadScene("Credits");

    }
    public void BtPlay()
    {
        SceneManager.LoadScene("Play");

    }
    public void BtStats()
    {
        SceneManager.LoadScene("Statistics");

    }
    public void BtLogin()
    {
        SceneManager.LoadScene("Home");

    }
    public void BtBath()
    {
        SceneManager.LoadScene("PlayBath");

    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }
    }
}
