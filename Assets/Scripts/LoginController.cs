using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoginController : MonoBehaviour {

	private static string URL = "https://dearpet.000webhostapp.com/login_service.php?";

	public InputField username;
	public InputField password; 

	// Use this for initialization
	void Start () {
        Screen.fullScreen = true;
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
		
		StartCoroutine(sendPlayer (username.text, password.text));

    }


	IEnumerator sendPlayer(string username, string password) {


		string login_attributes = "username=" + username + "&" +
			"password=" + password;

		Debug.Log ("Parametros: " + login_attributes);

		UnityWebRequest www = UnityWebRequest.Get(URL + login_attributes);

		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {

			//string json = System.Text.Encoding.ASCII.GetString(www.downloadHandler.data);
			string json = www.downloadHandler.text;


			Debug.Log (json);
			Message m = JsonUtility.FromJson<Message>(json.Replace("'", "\""));

			if (m.status.Equals("true")) {
				SceneManager.LoadScene("Home");
			}

		}

	}
}
