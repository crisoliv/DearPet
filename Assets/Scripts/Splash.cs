using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    
    AsyncOperation async;

    private void Awake()
    {
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("PlayerWords", 0);
        PlayerPrefs.SetInt("PlayerVerbs", 0);
       
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3);

        //async = SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive);
        //while (!SceneManager.GetSceneByName("Login").isLoaded)    // Wait until scene load completely.
        //{
        //    yield return null;
        //}
        //SceneManager.UnloadSceneAsync("Splash");  // Unload "SplashScreen" scene.
        //async.allowSceneActivation = true;
        SceneManager.LoadScene("Login");

        yield return async;
    }

   
    void Start()
    {

        StartCoroutine("Load"); //Loads Home Scene on the background.
    }

}
