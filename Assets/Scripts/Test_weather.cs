using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class Test_weather : MonoBehaviour
{

    private string url2 = "http://api.worldweatheronline.com/premium/v1/weather.ashx?key=cc053739d9194534af740717191803&q=LA&format=json&num_of_days=1";
    // Use this for initialization
    private string url1 = "https://api.aerisapi.com/forecasts/:auto?&format=json&filter=day&limit=1&client_id=dfmlcsJeDXfC32SpDtlnh&client_secret=zyK6bsDjSFVLe0zbQU2Mg8VUqp2eVBQ7GpyVm6Zh";
    private string weather_text;
    void Start()
    {

        StartCoroutine(ww());

    }

    IEnumerator ww()
    {
        WWW web1 = new WWW(url1);
        WWW web2 = new WWW(url2);
        yield return web1;
        yield return web2;
        if (web1.text != null&& web2.text != null)
        {
            JsonData json = JsonMapper.ToObject(web1.text);
            JsonData json2 = JsonMapper.ToObject(web2.text);
            /* weather_text = "City:           " + json["response"][0]["profile"]["tz"].ToString() + "\n" +
                            "Longitude: " + json["response"][0]["loc"]["long"].ToString() + "   " +
                            "Latitude:  " + json["response"][0]["loc"]["lat"].ToString() + "\n" +
                            "Validtime: " + json["response"][0]["periods"][0]["validTime"].ToString() + "\n" +
                            "Temp:        " + json["response"][0]["periods"][0]["minTempF"].ToString() + "℉ ~ " +
                                            json["response"][0]["periods"][0]["maxTempF"].ToString() + "℉  " +
                                            json["response"][0]["periods"][0]["minTempC"].ToString() + "℃ ~ " +
                                            json["response"][0]["periods"][0]["maxTempC"].ToString() + "℃ \n" +
                            "Humidity:  " + json["response"][0]["periods"][0]["minHumidity"].ToString() + "% ~ " +
                                            json["response"][0]["periods"][0]["maxHumidity"].ToString() + "%\n" +
                            "Weather:   " + json["response"][0]["periods"][0]["weather"].ToString();*/
            weather_text = "new:       " + json2["data"]["request"][0]["query"].ToString() + "\n" +
                           "picture:   " + json2["data"]["current_condition"][0]["weatherIconUrl"][0]["value"].ToJson();
            Application.OpenURL(json2["data"]["current_condition"][0]["weatherIconUrl"][0]["value"].ToJson());


            GetComponent<Text>().text = weather_text;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}