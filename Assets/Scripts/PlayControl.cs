using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Screen.fullScreen = false;
    }

    public void BtCustomization()
    {
        SceneManager.LoadScene("Customization");

    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
