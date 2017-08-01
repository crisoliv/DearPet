using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserLogin :MonoBehaviour {

	public static Player player; 

	private static string URL = "https://dearpet.000webhostapp.com/login_service.php?";


	// Use this for initialization
	void Start () {

		// passar os dados da view e passar como parametro aqui
		StartCoroutine(sendPlayer("taka", "123"));
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator sendPlayer(string username, string password) {

		string login_attributes = "username=" + username + "&" +
			"password=" + password;

		UnityWebRequest www = UnityWebRequest.Get(URL + login_attributes);

		yield return www.Send();

		/*if(www.isNetworkError) {
			Debug.Log(www.error);
		}
		else {*/

			//string json = System.Text.Encoding.ASCII.GetString(www.downloadHandler.data);
			string json = www.downloadHandler.text;
			Debug.Log (json);
//			player = JsonUtility.FromJson<Player>(json.Replace("'", "\""));
//
//			Debug.Log (player.Id);
//			Debug.Log (player.Username);

		//}

	}

}
