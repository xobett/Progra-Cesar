using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class WeatherAPI : MonoBehaviour
{
    //Struct para organizar los datos del clima.
    [SerializeField] WeatherData data;
    //Arreglo de structs que organizan la informacion de un pais.
    [SerializeField] private WeatherCountry[] countries = new WeatherCountry[10];
    //String no modificable que contiene la llave API.
    private static readonly string apiKey = "dd355587e331db0873d6e0b86b684739";

    //String donde se convierte la informacion Json.
    private string json;

    private void Start()
    {
        StartCoroutine(RetrieveWeatherData());
    }

    private string UpdatedURL()
    {
        WeatherCountry chosenCountry = countries[GetRandomCountry()];

        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={chosenCountry.countryLatitude}&lon={chosenCountry.countryLongitude}&appid={apiKey}&units=metric";

        return url;
    }

    IEnumerator RetrieveWeatherData()
    {
        yield return new WaitForSecondsRealtime(5);

        UnityWebRequest request = new UnityWebRequest(UpdatedURL());
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

            DecodeJson();
        }
    }


    private void ChangeEnvironment()
    {

    }

    private void DecodeJson()
    {
        var weatherJson = JSON.Parse(json);

        data.country = weatherJson["sys"]["country"].Value;
        data.name = weatherJson["name"].Value;
        data.actualTemp = float.Parse(weatherJson["main"]["temp"].Value);
        data.description = weatherJson["weather"][0]["description"].Value;
        data.windSpeed = float.Parse(weatherJson["wind"]["speed"].Value);
    }

    private int GetRandomCountry()
    {
        int randomCountryNumber = Random.Range(0, countries.Length);
        return randomCountryNumber;
    }
    

}
