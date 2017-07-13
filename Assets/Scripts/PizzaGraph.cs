using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PizzaGraph : MonoBehaviour {

    public float[] values;
    public Color[] wedgeColors;
    public Image wedgePrefab;
    public GameObject ListeningUI, ReadingUI;
	void Start () {
        ///TODO: EDITAR AS VARIAVEIS AQUI
        values[0] = 2;
        values[1] = 1;

        StartCoroutine(MakeGraph());	
	}
	
	
	IEnumerator MakeGraph()
    {
        int order = 0;
        float total = 0;
        float zRotation = 0;
        for(int i=0; i < values.Length; i++)
        {
            total += values[i];

        }

        for(int i = 0; i < values.Length; i++)
        {
            
            yield return new WaitForSeconds(.1F);
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = wedgeColors[i];
            newWedge.fillAmount = values[i] / total;
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));

           
            newWedge.canvasRenderer.SetAlpha(0.0f);
            newWedge.CrossFadeAlpha(1.0f, .4F, false);
            zRotation -= newWedge.fillAmount * 360f;
            while (newWedge.color.a != 1)
            {
                yield return new WaitForSeconds(.2F);
            }

            if (order == 0)
            {
                ListeningUI.SetActive(true);
                order++;
            }
            else
            {
                ReadingUI.SetActive(true);
            }
           

        }
        yield return null;


    }
}
