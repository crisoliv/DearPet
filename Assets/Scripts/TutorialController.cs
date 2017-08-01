using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialController : MonoBehaviour {

    public Text textBox;
   
    int order = 0;
    public GameObject apiBtn;
	void Start () {

        /*if (PlayerPrefs.HasKey("FirstTime"))    //Not the first time.
        {*/
            StartGame();
        /*}
        else // First time.
        {*/
            /*textBox.transform.parent.gameObject.SetActive(true);
            PlayerPrefs.SetInt("FirstTime",1);
            PlayerPrefs.Save();
            RunTutorial();*/
        //}
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
            textA = "Hi, my name is Boo. Nice to meet you! - Oi, meu nome é Boo. Prazer em conhecê-lo!";
        }
        else if (order == 2)
        {
            textA = "You can use this screen to feed me! - Você pode usar esta tela para me alimentar!";
        }
        else if (order == 3)
        {
            textA = "Can you see the GREEN BAR below me? That's my STAMINA! - A BARRA VERDE abaixo é a minha STAMINA!";            
        }
        else if (order == 4)
        {

            textA = "When it starts to decrease too much, you should feed me! - Quando diminuir demais, me alimente!";

        }
        else if (order == 5)
        {

            textA = "To feed me, touch the food above and pronounce correctly! - Para me alimentar, toque na comida acima e pronuncie corretamente!";

        }
        else
        {
            StartGame();
            return;
        }

        textBox.text = textA;
    }
}
