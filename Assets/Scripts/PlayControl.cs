using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour {

    bool whichItem = true;
    bool customUsing = true;
    public Camera camera;
    RectTransform customRect;

    public GameObject titleDearPet;
    public GameObject titleCustom;

    public GameObject itemPlay;
    public GameObject itemCustom;
    public GameObject itemCustomUsing;

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
        Screen.fullScreen = true;
        customRect = itemCustom.GetComponent<RectTransform>();
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
        } else {
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

    public void BtUseItemCustomization()
    {
        /*customRect.localPosition = new Vector3 (0, -355, 0);
        print(customRect.localPosition);*/
        
        //if (customUsing)
        //{
            itemCustomUsing.SetActive(true);
        //}
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
