using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeController : MonoBehaviour {

    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstApple")) PlayerPrefs.DeleteKey("FirstApple");

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
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
