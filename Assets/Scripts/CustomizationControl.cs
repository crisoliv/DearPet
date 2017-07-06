using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationControl : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Screen.fullScreen = true;
    }

    public void BtBack()
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
