using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class WeatherAPI : MonoBehaviour
{
    [SerializeField] WeatherData data;
    [SerializeField] private float latitud = 37.566f;
    [SerializeField] private float longitud = 126.9784f;
    private static readonly string apiKey = "7fe45acb4f5a69f83c45312aad97613a";

    private string url;

    private string json;
    void Start()
    {
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitud}&lon={longitud}&appid={apiKey}&lang=sp&units=metric";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RetrieveWeatherData()
    {
        yield return new WaitForSecondsRealtime(60);

        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            json = request.downloadHandler.text;
        }
    }

    private void DecodeJson()
    {
        var weatherJson = JSON.Parse(json);

        data.timeZone = weatherJson["timeZone"].Value;
        data.actualTemp = float.Parse(weatherJson["current"]["temp"].Value);
        data.description = weatherJson["current"]["weather"][0]["description"].Value;
        data.windSpeed = float.Parse(weatherJson["current"]["wind_speed"].Value);
    }
}
