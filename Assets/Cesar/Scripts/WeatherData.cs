using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    [SerializeField] public string country;
    [SerializeField] public string name;
    [SerializeField] public string description;

    [SerializeField] public float actualTemp;
    [SerializeField] public float windSpeed;
}
