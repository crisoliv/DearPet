using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoginController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.fullScreen = false;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
   
    public void BtEntrar()
    {
        SceneManager.LoadScene("Home");

    }
}
