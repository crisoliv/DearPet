using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeController : MonoBehaviour {

    void Start()
    {
        Screen.fullScreen = false;
    }
    public void BtCredits()
    {
        SceneManager.LoadScene("Credits");

    }
    public void BtPlay()
    {
        SceneManager.LoadScene("Play");

    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
