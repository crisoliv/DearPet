using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour {

    bool whichItem = true;
    public Camera camera;

    public GameObject titleDearPet;
    public GameObject titleCustom;

    public GameObject itemPlay;
    public GameObject itemCustom;

    public GameObject itemPlayBack;
    public GameObject itemCustomBack;

    public GameObject txtCustom;
    public GameObject txtBack;

    public GameObject bar;

    public Color colorWhite = Color.white;
    public Color colorBlue;


    // Use this for initialization
    void Start()
    {
        Screen.fullScreen = false;
        //camera = GetComponent<Camera>();
    }

    public void BtCustomization()
    {
        /*SceneManager.LoadScene("Customization");*/
        if (whichItem)
        {
            titleDearPet.SetActive(false);
            titleCustom.SetActive(true);

            itemCustom.SetActive(true);
            itemCustomBack.SetActive(true);
            txtCustom.SetActive(false);

            itemPlay.SetActive(false);
            itemPlayBack.SetActive(false);
            txtBack.SetActive(true);

            bar.SetActive(false);

            camera.backgroundColor = colorWhite;

            whichItem = false;
        }else {
            titleDearPet.SetActive(true);
            titleCustom.SetActive(false);

            itemPlay.SetActive(true);
            itemPlayBack.SetActive(true);
            txtCustom.SetActive(true);

            itemCustom.SetActive(false);
            itemCustomBack.SetActive(false);
            txtBack.SetActive(false);

            bar.SetActive(true);

            camera.backgroundColor = colorBlue;

            whichItem = true;
        }        
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
