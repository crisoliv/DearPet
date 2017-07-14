﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialController : MonoBehaviour {

    public Text textBox;
   
    int order = 0;
    public GameObject apiBtn;
	void Start () {

        PlayerPrefs.DeleteKey("FirstTime");
        PlayerPrefs.Save();



        if (PlayerPrefs.HasKey("FirstTime"))    //Not the first time.
        {
            StartGame();
        }
        else // First time.
        {
            textBox.transform.parent.gameObject.SetActive(true);
            PlayerPrefs.SetInt("FirstTime",1);
            PlayerPrefs.Save();
            RunTutorial();
        }
    }


    void StartGame()
    {

        Destroy(textBox.transform.parent.gameObject);
        GetComponent<PlayControl>().enabled = true;
        apiBtn.SetActive(true);

        GetComponent<TutorialController>().enabled = false;
    }

    public void RunTutorial()
    {
        order++;
        string textA = "";
        if (order == 1)
        {
            textA = "Hi buddy, you can use this screen to feed me!";


        }
        else if (order == 2)
        {
            textA = "In this green bar below, you can see my stamina...";
        }
        else if (order == 3)
        {

            textA = "When it starts to decrease too much you should feed me!";

        }
        else if (order == 4)
        {

            textA = "To feed me, touch the food on top of my head and say the right pronunciation!";

        }
        else
        {
            StartGame();
            return;
        }

        textBox.text = textA;


     





    }
}
