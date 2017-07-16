using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour {

    bool whichItem = true;
    bool customUsing = true;
    int nextItemCustom = 0;
    public Camera camera;
    RectTransform customRect;

    public GameObject titleDearPet;
    public GameObject titleCustom;

    public GameObject btTalk;
    public GameObject btItemCustom;
    public GameObject btNextItemCustom;
    public GameObject btPrevItemCustom;

    public GameObject itemPlay;
    public GameObject itemCustom;
    public GameObject itemCustomUsing;
    public GameObject itemCustom2;
    public GameObject itemCustomUsing2;

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

        if (GameObject.Find("HappyWinPS"))
        {
            GameObject.Find("HappyWinPS").SetActive(false);
        }
        GameObject.Find("Pet").GetComponent<Animator>().SetBool("jump",false);


        if (whichItem)
        {
            titleDearPet.SetActive(false);
            titleCustom.SetActive(true);

            btTalk.SetActive(false);
            btItemCustom.SetActive(true);
            btNextItemCustom.SetActive(true);
            btPrevItemCustom.SetActive(true);

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

            btTalk.SetActive(true);
            btItemCustom.SetActive(false);
            btNextItemCustom.SetActive(false);
            btPrevItemCustom.SetActive(false);

            itemPlay.SetActive(true);
            itemPlayBack.SetActive(true);
            txtCustom.SetActive(true);

            itemCustom.SetActive(false);
            itemCustom2.SetActive(false);
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
        
        if (nextItemCustom == 0)
        {
            itemCustomUsing.SetActive(true);
            itemCustomUsing2.SetActive(false);
        }
        else {
            itemCustomUsing2.SetActive(true);
            itemCustomUsing.SetActive(false);
        }
    }

    public void BtNextItem()
    {
        /*customRect.localPosition = new Vector3 (0, -355, 0);
        print(customRect.localPosition);*/

        if (nextItemCustom == 0)
        {
            itemCustom.SetActive(false);
            itemCustom2.SetActive(true);
            nextItemCustom++;
        }
        else
        {
            itemCustom.SetActive(true);
            itemCustom2.SetActive(false);
            nextItemCustom--;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }
    }
}
