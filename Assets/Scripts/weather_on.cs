using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class weather_on : MonoBehaviour {
	private string url = "https://api.aerisapi.com/forecasts/:auto?&format=json&filter=day&limit=1&client_id=dfmlcsJeDXfC32SpDtlnh&client_secret=zyK6bsDjSFVLe0zbQU2Mg8VUqp2eVBQ7GpyVm6Zh";
	public static string temp_min;
	public static string temp_max;
	public static string weather;
	public static string city;
	public static string icon;
	void Start()
	{
		//        this.transform.gameObject.SetActive(false);
		StartCoroutine(ww());
		this.transform.gameObject.SetActive(true);

	}
	// Use this for initialization
	void Update()
	{
		if (GameManager.selectedButton&&GameManager.selectedButton.name == "weather")
		{
			StartCoroutine(ww());
			this.transform.gameObject.SetActive(true);
			GameManager.selectedButton = null;

		}
		if (GameManager.selectedButton&&GameManager.selectedButton.name == "close_button")
		{
			this.transform.gameObject.SetActive(false);
			GameManager.selectedButton = null;
		}

	}
	IEnumerator ww()
	{
		WWW web = new WWW(url);
		yield return web;
		if (web.text != null)
		{
			JsonData json = JsonMapper.ToObject(web.text);
			//			weather_text = "City:           " + json["response"][0]["profile"]["tz"].ToString() + "\n" +
			//				"Longitude: " + json["response"][0]["loc"]["long"].ToString()+ "   "+
			//				"Latitude:  " + json["response"][0]["loc"]["lat"].ToString()+"\n"+
			//				"Validtime: " + json["response"][0]["periods"][0]["validTime"].ToString() + "\n" +
			//				"Temp:        " + json["response"][0]["periods"][0]["minTempF"].ToString() + "℉ ~ " +
			//				json["response"][0]["periods"][0]["maxTempF"].ToString() + "℉  " +
			//				json["response"][0]["periods"][0]["minTempC"].ToString() + "℃ ~ " +
			//				json["response"][0]["periods"][0]["maxTempC"].ToString() + "℃ \n" +
			//				"Humidity:  " + json["response"][0]["periods"][0]["minHumidity"].ToString() + "% ~ " +
			//				json["response"][0]["periods"][0]["maxHumidity"].ToString() + "%\n" +
			//				"Weather:   " + json["response"][0]["periods"][0]["weather"].ToString();

			temp_min = json ["response"] [0] ["periods"] [0] ["minTempC"].ToString () + "℃";
			temp_max = json ["response"] [0] ["periods"] [0] ["maxTempC"].ToString () + "℃";
			weather = json["response"][0]["periods"][0]["weather"].ToString();
			city = json ["response"] [0] ["profile"] ["tz"].ToString().Split('/')[1];
			icon = json["response"][0]["periods"][0]["icon"].ToString().Split('.')[0];
			//			Debug.LogError (city + temp_min + temp_max + weather + icon);
			foreach (Transform child in this.transform)
			{
				string text_name = child.gameObject.name;
				if (text_name == "city_text") {
					child.gameObject.GetComponent<Text> ().text = "<color=white>" + weather_on.city + "</color>";
				} else if (text_name == "weather_text") {
					child.gameObject.GetComponent<Text> ().text = weather_on.weather;
				} else if (text_name == "temperature_text") {
					child.gameObject.GetComponent<Text> ().text = weather_on.temp_min + " ~ " + weather_on.temp_max;
				} else if (text_name == "weather_png") {
					child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("WeatherIcon/" + weather_on.icon);
				}
			}


		}

	}
}
