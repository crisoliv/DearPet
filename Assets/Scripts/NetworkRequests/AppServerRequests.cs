using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppServerRequests : MonoBehaviour{

	private static string BASE_URL = "https://dearpet.000webhostapp.com/";
	private static string GET_STATS = "/get_stats_service.php?";
	private static string UPDATE_STATS = "/increment_wrongrighttype_service.php?";
	private static string NEW_USER = "/increment_wrongrighttype_service.php?";


	public void getStats(int playerid){
		string request = BASE_URL + GET_STATS + "playerid=" + playerid;
		StartCoroutine (statsRequest(request));
	}

	IEnumerator statsRequest(string request) {


		UnityWebRequest www = UnityWebRequest.Get(request);

		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {

			//string json = System.Text.Encoding.ASCII.GetString(www.downloadHandler.data);
			string json = www.downloadHandler.text;
			Debug.Log (json);
//			player = JsonUtility.FromJson<Player>(json.Replace("'", "\""));
//
//			Debug.Log (player.Id);
//			Debug.Log (player.Username);

		}

	}

}
