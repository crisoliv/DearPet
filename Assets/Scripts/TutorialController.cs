using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialController : MonoBehaviour {

    public Text textBox;
   
    int order = 0;
    public GameObject apiBtn;
	void Start () {

        /*if (PlayerPrefs.HasKey("FirstTime"))    //Not the first time.
        {*/
            //StartGame();
        /*}
        else // First time.
        {*/
            textBox.transform.parent.gameObject.SetActive(true);
            PlayerPrefs.SetInt("FirstTime",1);
            PlayerPrefs.Save();
            RunTutorial();
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
        if (Application.loadedLevelName == "Play")
        {
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
                textA = "Can you see the GREEN BAR below? That's my STAMINA! [...]";
            }
            else if (order == 4)
            {
                textA = "[...] - A BARRA VERDE abaixo representa a minha estamina!";
            }
            else if (order == 5)
            {

                textA = "When it decrease too much, you should feed me! - Quando diminuir demais, me alimente!";

            }
            else if (order == 6)
            {

                textA = "To feed me, touch the food and pronounce it correctly! [...]";

            }
            else if (order == 7)
            {

                textA = "[...] - Para me alimentar, toque na comida e pronuncie corretamente!";

            }
            else
            {
                StartGame();
                return;
            }
        }else
        {
            if (order == 1)
            {
                textA = "Hi again! - Oi de novo!";
            }
            else if (order == 2)
            {
                textA = "You can use this screen to give me a bath! - Você pode usar esta tela para me dar banho!";
            }
            else if (order == 3)
            {
                textA = "Can you see the BLUE BAR below? That's my HYGIENE's bar! [...]";
            }            
            else if (order == 4)
            {
                textA = "[...] - A BARRA AZUL abaixo representa a minha higiene!";
            }
            else if (order == 5)
            {

                textA = "When it decrease too much, you should give me a bath! - Quando diminuir demais, me dê banho!";

            }
            else if (order == 6)
            {

                textA = "To give me a bath, touch the bathroom's object above and pronounce it correctly! [...]";
                
            }
            else if (order == 7)
            {

                textA = "[...] - Para me dar banho, toque na objeto acima e pronuncie corretamente!";
                
            }
            else
            {
                StartGame();
                return;
            }
        }
        textBox.text = textA;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }
    }
}
